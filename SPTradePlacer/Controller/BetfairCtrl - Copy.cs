using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HtmlAgilityPack;
using System.IO;
using WebSocketSharp;
using System.Runtime.Serialization.Formatters.Binary;
using OrbitBot.Json;

namespace OrbitBot.Controller
{
    public class BetfairCtrl1
    {
        public Account m_account;
        public bool m_isLogged = false;

        protected onWriteStatusEvent m_handlerWriteStatus;
        protected onWriteLogEvent m_handlerWriteLog;
        protected string m_domain = "https://www.orbitxch.com";
        protected CookieContainer m_cookieContainer;
        protected HttpClient m_httpClient = null;
        protected Thread _mainThread = null;
        protected bool _bWorking = false;
        protected Dictionary<string, Thread> _childrenThreadDict = new Dictionary<string, Thread>();
        private int _threadCounter = -1;
        private List<JsonRace> todaysRaceList = new List<JsonRace>();

        public BetfairCtrl(Account account, onWriteStatusEvent onWriteStatus, onWriteLogEvent onWriteLog)
        {
            m_account = account;
            m_handlerWriteStatus = onWriteStatus;
            m_handlerWriteLog = onWriteLog;
            m_cookieContainer = new CookieContainer();
            ReadCookiesFromDisk();
            InitHttpClient();
        }

        public double getBalance()
        {
            double balance = -1;
            try
            {
                string urlBalance = m_domain + "/customer/api/account/balance";
                HttpResponseMessage responseMessageLogin = m_httpClient.GetAsync(urlBalance).Result;
                responseMessageLogin.EnsureSuccessStatusCode();
                string content = responseMessageLogin.Content.ReadAsStringAsync().Result;
                dynamic jsonMessage = JsonConvert.DeserializeObject<dynamic>(content);
                balance = Utils.ParseToDouble(jsonMessage["balance"].ToString());
            }
            catch (Exception e)
            {
                m_handlerWriteStatus("Exception in getBalance: " + e.ToString());
            }

            return balance;
        }

        public void StartWorker()
        {
            _bWorking = true;
            if (_mainThread != null)
                _mainThread.Abort();
            _mainThread = new Thread(() => mainWorkerFunc());
            _mainThread.Start();
        }

        public void StopWorker()
        {
            try
            {
                _bWorking = false;
                _mainThread.Abort();
                foreach(Thread thread in _childrenThreadDict.Values)
                {
                    try
                    {
                        thread.Abort();
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        /*
         *  *** The purpose of this function ***
         *   1. Refresh today's races every 1 hour.
         *   2. Filter races before 5 mins and start child threads to find opportunities.
         *  
        */
        protected void mainWorkerFunc()
        {
            while (true)
            {
                try
                {

                    if(_threadCounter > 60*30 || _threadCounter == -1)
                    {
                        double currentBalance = getBalance();
                        if(currentBalance < 0)
                        {
                            doLogin();
                            currentBalance = getBalance();
                        }
                        m_handlerWriteStatus(string.Format("Current Balance:{0}", currentBalance));

                        _threadCounter = 0;

                        string todayRacesLink = "https://apieds.betfair.com.au/api/eds/racing-navigation/v1?_ak=nzIFcwyWhrlwYMrh&eventTypeId=7&navigationType=todayscard&raceId=11111111.1111";
                        HttpResponseMessage todaysResp = m_httpClient.GetAsync(todayRacesLink).Result;
                        todaysResp.EnsureSuccessStatusCode();
                        string strTodayscard = todaysResp.Content.ReadAsStringAsync().Result;
                        //m_handlerWriteStatus(strTodayscard);
                        dynamic jsonMessage = JsonConvert.DeserializeObject<dynamic>(strTodayscard);
                        JsonRace[] raceList = JsonConvert.DeserializeObject<JsonRace[]>(jsonMessage.races.ToString());
                        todaysRaceList = raceList.ToList();
                    }
                    _threadCounter++;
                    foreach (JsonRace raceItem in todaysRaceList)
                    {
                        double diffSeconds = raceItem.getRemainSeconds();
                        if (diffSeconds <= Setting.instance.beforeKickOff)
                        {
                            if (!_childrenThreadDict.ContainsKey(raceItem.winMarketId))
                            {
                                Thread childThread = new Thread(() => childThreadFunc(raceItem));
                                childThread.Start();
                                _childrenThreadDict.Add(raceItem.winMarketId, childThread);
                            }
                        }
                    }
                }
                catch
                {

                }
                Thread.Sleep(1000);
            }
        }

        /*
         *  *** It finds candidates with below 6.00 odds
         *   - updating volumn page at every seconds and find opportunity
         */
        protected void childThreadFunc(JsonRace raceItem)
        {

            m_handlerWriteStatus(string.Format("New Trade is Started! : {0}", raceItem.meetingName));
            List<JObject> bestRunners = getBestRunners(ref raceItem);
            Dictionary<string, JObject> dictBestRunners = new Dictionary<string, JObject>();
            foreach (JObject runner in bestRunners)
            {
                string selectionId = runner.SelectToken("selectionId").ToString();
                createNewTrade(raceItem, runner);
                dictBestRunners.Add(selectionId, runner);
            }
            bool bAllOrdersMatched = true;
            while (raceItem.getRemainSeconds() > 5)
            {
                bAllOrdersMatched = true;
                try
                {
                    string marketPosUrl = m_domain + "/www/sports/exchange/reporting/live/v1.0/getMarketPositionViews?_ak=nzIFcwyWhrlwYMrh&alt=json&includeSettledProfit=false&marketIds={0}&matchProjection=MATCH";
                    marketPosUrl = string.Format(marketPosUrl, raceItem.winMarketId);
                    HttpResponseMessage marketPostResp = m_httpClient.GetAsync(marketPosUrl).Result;
                    marketPostResp.EnsureSuccessStatusCode();
                    string strMarketPos = marketPostResp.Content.ReadAsStringAsync().Result;
                    dynamic jsonMessage = JsonConvert.DeserializeObject<dynamic>(strMarketPos);

                    bestRunners = getAllRunners(ref raceItem);
                    foreach (JObject runner in bestRunners)
                    {
                        string selId = runner.SelectToken("selectionId").ToString();
                        if (dictBestRunners.ContainsKey(selId))
                            dictBestRunners[selId] = runner;
                        else
                            dictBestRunners.Add(selId, runner);
                    }

                    foreach (var selection in jsonMessage[0].selections)
                    {
                        string selectionId = selection.selectionId.ToString();

                        bool bAllMatched = true;
                        foreach (var order in selection.orders)
                        {
                            string sizeRemaining = order.sizeRemaining.ToString();
                            if (!sizeRemaining.Equals("0"))
                            {
                                bAllMatched = false;
                                bAllOrdersMatched = false;
                            }
                        }
                        if (bAllMatched && selection.orders.Length <= 2)
                        {
                            createNewTrade(raceItem, dictBestRunners[selectionId]);
                        }
                    }
                }
                catch
                {
                }

                if (bAllOrdersMatched)
                {
                    bool bGotProfit = cancelBet(raceItem, true);
                }
                Thread.Sleep(500);
            }

            while (raceItem.getRemainSeconds() >= -45)
            {
                bool bGotProfit = cancelBet(raceItem, true);
                if (bGotProfit)
                    break;
                Thread.Sleep(500);
            }
            cancelBet(raceItem);
            m_handlerWriteStatus(string.Format("Trade is Finished! : {0}", raceItem.meetingName));
        }

        protected double getLayPriceMatched(JsonRace raceItem, string layBetId)
        {
            string marketPosUrl = m_domain + "/www/sports/exchange/reporting/live/v1.0/getMarketPositionViews?_ak=nzIFcwyWhrlwYMrh&alt=json&includeSettledProfit=false&marketIds={0}&matchProjection=MATCH";
            marketPosUrl = string.Format(marketPosUrl, raceItem.winMarketId);
            HttpResponseMessage marketPostResp = m_httpClient.GetAsync(marketPosUrl).Result;
            marketPostResp.EnsureSuccessStatusCode();
            string strMarketPos = marketPostResp.Content.ReadAsStringAsync().Result;
            dynamic jsonMessage = JsonConvert.DeserializeObject<dynamic>(strMarketPos);
            foreach (var selection in jsonMessage[0].selections)
            {
                foreach (var order in selection.orders)
                {
                    double averagePriceMatched = Utils.ParseToDouble(order.averagePriceMatched.ToString());
                    string betId = order.betId.ToString();
                    if (betId.EndsWith(layBetId))
                    {
                       return averagePriceMatched;
                    }
                }
            }
            return 0;
        }

        protected void createNewTrade(JsonRace raceItem, dynamic runner)
        {
            double layPrice = 0;
            string layBetId = string.Empty;
            string backBetId = string.Empty;
            bool bLaySuccess = placeBet(raceItem, runner, ref layBetId, ref layPrice, true);
            bool bBackSuccess = false;
            if (bLaySuccess) 
            {
                double matchedPrice = getLayPriceMatched(raceItem, layBetId);
                if (matchedPrice > 0)
                    layPrice = matchedPrice;
                m_handlerWriteStatus(string.Format("Accepted lay price: {0}", matchedPrice));
                bBackSuccess = placeBet(raceItem, runner, ref backBetId, ref layPrice);
            }
            if (!bBackSuccess)
                doCancelUnmatchedBet(layBetId, raceItem.winMarketId);
        }

       public bool cancelBet(JsonRace raceItem, bool cashoutPositive = false)
        {
            int cashoutTry = 0;
            while (raceItem.getRemainSeconds() > -180)
            {
                try
                {
                    string quoteUrl = "https://cashout-service.betfair.com.au/cashout-service/readonly/v1.0/quote?_ak=nzIFcwyWhrlwYMrh&alt=json&currencyCode=AUD&marketIds=" + raceItem.winMarketId;
                    HttpResponseMessage marketPostResp = m_httpClient.GetAsync(quoteUrl).Result;
                    marketPostResp.EnsureSuccessStatusCode();
                    string strMarketPos = marketPostResp.Content.ReadAsStringAsync().Result;
                    
                    dynamic jsonMessage = JsonConvert.DeserializeObject<dynamic>(strMarketPos);
                    if (jsonMessage[0].status.ToString().Equals("UNAVAILABLE")) 
                    {
                        if (cashoutPositive) return false;
                        Thread.Sleep(500);
                        continue;
                    }
                    string availableCashout = jsonMessage[0].value.ToString();
                    string profit = jsonMessage[0].profit.ToString();
                    if (availableCashout.Equals("0")) 
                    {
                        if (cashoutPositive) return false;
                        Thread.Sleep(500);
                        continue;
                    } 

                    if(cashoutPositive && Utils.ParseToDouble(profit) < 0) return false;

                    string cashoutUrl = string.Format("https://cashout-service.betfair.com.au/cashout-service/transactional/v1.0/cashout?_ak=nzIFcwyWhrlwYMrh&alt=json&currencyCode=AUD&marketId={0}&quotePercentage=100&quoteValue={1}",
                        raceItem.winMarketId, availableCashout);

                    marketPostResp = m_httpClient.GetAsync(cashoutUrl).Result;
                    marketPostResp.EnsureSuccessStatusCode();
                    strMarketPos = marketPostResp.Content.ReadAsStringAsync().Result;
                    if (strMarketPos.Contains("SUCCESS"))
                    {
                        cashoutTry++;
                        m_handlerWriteStatus(string.Format("(X) CANCEL => {0} {1} profit: {2}", raceItem.meetingName, availableCashout, profit));
                        if (cashoutTry >= 3) return true;
                        Thread.Sleep(500);
                    }
                }
                catch
                {

                }
            }
            return false;
        }
        
        public bool placeBet(JsonRace raceItem, JObject runner, ref string betId, ref double layPrice, bool bLay = false)
        {
            try
            {
                string marketId = raceItem.winMarketId;
                dynamic exchange = runner.SelectToken("exchange").ToObject<dynamic>();
                dynamic oddsValue = exchange.availableToBack[0];
                if(bLay)
                    oddsValue = exchange.availableToLay[0];

                string strMarketReferer = $"";
                long timestamp = Utils.getTick();
                string paramLink = $"{timestamp}-{marketId}-plc-0";

                dynamic betRequest = new JObject();
                betRequest.id = $"{marketId}-plc";
                betRequest.method = "ExchangeTransactional/v1.0/place";
                //betRequest.method = "SportsAPING/v1.0/placeOrders";
                betRequest.jsonrpc = "2.0";

                double stake = Setting.instance.flatStake;

                double price = Utils.ParseToDouble(oddsValue.price.ToString());
                if (!bLay) 
                {
                    if(layPrice < 3)
                    {
                        price = layPrice + 0.02;
                        price = Math.Floor(price * 100) / 100;
                    }
                    else if (layPrice < 4)
                    {
                        price = layPrice + 0.05;
                        price = Math.Floor(price * 100);
                        price = price - price % 5;
                        price = price / 100;
                    }
                    else
                    {
                        price = layPrice + 0.1;
                        price = Math.Floor(price * 10);
                        price = price / 10;
                    }
                }
                if(price >= Setting.instance.maxOdds - 0.2)
                {
                    return false;
                }
                layPrice = price;

                dynamic betLimitOrder = new JObject();
                betLimitOrder.size = stake;
                betLimitOrder.price = (price).ToString();
                betLimitOrder.persistenceType = "PERSIST";
                //betLimitOrder.timeInForce = "FILL_OR_KILL";
                //betLimitOrder.minFillSize = 1;

                string selectionId = runner.SelectToken("selectionId").ToString();
                dynamic betReqParam = new JObject();
                betReqParam.selectionId = long.Parse(selectionId);
                betReqParam.orderType = "LIMIT";
                betReqParam.side = "BACK";
                if(bLay)
                    betReqParam.side = "LAY";
                betReqParam.limitOrder = betLimitOrder;

                JArray reqParams = new JArray();
                reqParams.Add(betReqParam);

                betRequest.betParams = new JObject();
                betRequest.betParams.marketId = marketId;
                betRequest.betParams.customerRef = paramLink;
                betRequest.betParams.useAvailableBonus = false;
                betRequest.betParams.instructions = reqParams;

                var jsonData = $"[{JsonConvert.SerializeObject(betRequest)}]";
                jsonData = jsonData.Replace("betParams", "params");
                //m_handlerWriteStatus("Request:");
                //m_handlerWriteStatus(jsonData.ToString());

                var data = new StringContent(jsonData, Encoding.UTF8, "application/json");

                string eventUrl = string.Format(m_domain + "/exchange/plus/horse-racing/market/{0}?nodeId={1}", raceItem.winMarketId, raceItem.raceId);
                m_httpClient.DefaultRequestHeaders.Referrer = new Uri(eventUrl);

                string bettingUrl = $"https://etx.betfair.com.au/www/etx-json-rpc?_ak=nzIFcwyWhrlwYMrh&alt=json";
                m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("content-type", "application/json");
                HttpResponseMessage responseMessage = m_httpClient.PostAsync(bettingUrl, data).Result;
                string strBetResult = responseMessage.Content.ReadAsStringAsync().Result;
                JArray betResult = JsonConvert.DeserializeObject<JArray>(strBetResult);
                if (betResult[0].SelectToken("result") == null)
                {
                    m_handlerWriteStatus(strBetResult);
                    m_handlerWriteStatus("Placement response is null.");
                    return false;
                }
                //m_handlerWriteStatus("Response");
                //m_handlerWriteStatus(strBetResult);
                string betStatus = betResult[0].SelectToken("result").SelectToken("status").ToString();

                if (betStatus != "SUCCESS")
                {
                    m_handlerWriteStatus("Placement result is not 'SUCCESS'.");
                    m_handlerWriteStatus(strBetResult);
                    return false;
                }
                betId = betResult[0].SelectToken("result").SelectToken("instructionReports")[0].SelectToken("betId").ToString();
                layPrice = Utils.ParseToDouble(betResult[0].SelectToken("result").SelectToken("instructionReports")[0].SelectToken("instruction").SelectToken("limitOrder").SelectToken("price").ToString());
                double sizeMatched = Utils.ParseToDouble(betResult[0].SelectToken("result").SelectToken("instructionReports")[0].SelectToken("sizeMatched").ToString());
                if (bLay)
                {
                    if (sizeMatched < stake)
                    {
                        m_handlerWriteStatus(string.Format("Detect Unmatched BET sizeMatched : {0} Odds : {1}", sizeMatched, layPrice));
                        doCancelUnmatchedBet(betId, marketId);
                        return false;
                    }
                }

                m_handlerWriteStatus(string.Format("(+) ADD {0} {1} => {2} at {3}: {4}",
                    raceItem.meetingName,
                    runner.SelectToken("description").SelectToken("runnerName").ToString(),
                    bLay ? "LAY" : "BACK",
                    layPrice,
                    betId)); 
                return true;
            }
            catch (Exception e)
            {
                m_handlerWriteStatus("Exception in placeBet: " + e.ToString());
                return false;
            }
        }

        private void doCancelUnmatchedBet(string betId, string marketId)
        {
            try
            {
                long timestamp = Utils.getTick();
                string paramLink = $"{timestamp}-{marketId}-cnl-0";
                if (string.IsNullOrEmpty(betId)) return;
                dynamic betRequest = new JObject();
                betRequest.id = $"{marketId}-cnl";
                betRequest.method = "ExchangeTransactional/v1.0/cancel";
                betRequest.jsonrpc = "2.0";

                dynamic betReqParam = new JObject();
                betReqParam.betId = betId;

                JArray reqParams = new JArray();
                reqParams.Add(betReqParam);

                betRequest.betParams = new JArray();
                betRequest.betParams.Add(marketId);
                betRequest.betParams.Add(reqParams);
                betRequest.betParams.Add(paramLink);

                var jsonData = $"[{JsonConvert.SerializeObject(betRequest)}]";
                jsonData = jsonData.Replace("betParams", "params");
                //m_handlerWriteStatus("Cancel Request:");
                //m_handlerWriteStatus(jsonData.ToString());

                var data = new StringContent(jsonData, Encoding.UTF8, "application/json");

                string bettingUrl = $"https://etx.betfair.com.au/www/etx-json-rpc?_ak=nzIFcwyWhrlwYMrh&alt=json";
                m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("content-type", "application/json");
                HttpResponseMessage responseMessage = m_httpClient.PostAsync(bettingUrl, data).Result;
                string strBetResult = responseMessage.Content.ReadAsStringAsync().Result;
                //m_handlerWriteStatus("Cancel Response");
                //m_handlerWriteStatus(strBetResult);
            }
            catch (Exception e)
            {
                m_handlerWriteStatus("Exception in doCancelUnmatchedBet: " + e.ToString());
            }
        }

        protected List<JObject> getBestRunners(ref JsonRace raceItem)
        {
            List<JObject> bestRunners = new List<JObject>();
            try
            {
                string endpoint = m_domain + "/www/sports/exchange/readonly/v1/bymarket?_ak=nzIFcwyWhrlwYMrh&alt=json&currencyCode=EUR&locale=en&marketIds=" + raceItem.winMarketId
                      + "&rollupLimit=25&rollupModel=STAKE&types=MARKET_STATE,MARKET_RATES,MARKET_DESCRIPTION,EVENT,RUNNER_DESCRIPTION,RUNNER_STATE,RUNNER_EXCHANGE_PRICES_BEST,RUNNER_METADATA,MARKET_LICENCE,MARKET_LINE_RANGE_INFO";
                HttpResponseMessage raceWinMarketResp = m_httpClient.GetAsync(endpoint).Result;
                raceWinMarketResp.EnsureSuccessStatusCode();
                string content = raceWinMarketResp.Content.ReadAsStringAsync().Result;
                JObject marketResult = JsonConvert.DeserializeObject<JObject>(content);
                dynamic state = marketResult["eventTypes"][0]["eventNodes"][0]["marketNodes"][0]["state"].ToObject<dynamic>();
                if (state.inplay.ToString() == "inplay")
                {
                    raceItem.startTime = DateTime.Parse(state.lastMatchTime.ToString());
                    raceItem.inplay = true;
                }

                JArray runners = marketResult["eventTypes"][0]["eventNodes"][0]["marketNodes"][0]["runners"].ToObject<JArray>();
                foreach (JObject runner in runners)
                {
                    if (runner["exchange"] == null || runner["exchange"]["availableToLay"] == null) continue;
                    double layOdds = double.Parse(runner["exchange"]["availableToLay"][0]["price"].ToString());
                    double backOdds = double.Parse(runner["exchange"]["availableToBack"][0]["price"].ToString());
                    //double avgOdds = (layOdds + backOdds) / 2;
                    double avgOdds = Math.Max(backOdds, layOdds);
                    if (avgOdds < Setting.instance.maxOdds && avgOdds > Setting.instance.minOdds)
                    {
                        bestRunners.Add(runner);
                        if (bestRunners.Count >= Setting.instance.runners) break;
                    }
                }
            }
            catch
            {

            }
            return bestRunners;
        }

        protected List<JObject> getAllRunners(ref JsonRace raceItem)
        {
            string endpoint = m_domain + "/www/sports/exchange/readonly/v1/bymarket?_ak=nzIFcwyWhrlwYMrh&alt=json&currencyCode=EUR&locale=en&marketIds=" + raceItem.winMarketId
                      + "&rollupLimit=25&rollupModel=STAKE&types=MARKET_STATE,MARKET_RATES,MARKET_DESCRIPTION,EVENT,RUNNER_DESCRIPTION,RUNNER_STATE,RUNNER_EXCHANGE_PRICES_BEST,RUNNER_METADATA,MARKET_LICENCE,MARKET_LINE_RANGE_INFO";
            HttpResponseMessage raceWinMarketResp = m_httpClient.GetAsync(endpoint).Result;
            raceWinMarketResp.EnsureSuccessStatusCode();
            string content = raceWinMarketResp.Content.ReadAsStringAsync().Result;
            JObject marketResult = JsonConvert.DeserializeObject<JObject>(content);
            dynamic state = marketResult["eventTypes"][0]["eventNodes"][0]["marketNodes"][0]["state"].ToObject<dynamic>();
            if (state.inplay.ToString() == "inplay")
            {
                raceItem.startTime = DateTime.Parse(state.lastMatchTime.ToString());
                raceItem.inplay = true;
            }

            JArray runners = marketResult["eventTypes"][0]["eventNodes"][0]["marketNodes"][0]["runners"].ToObject<JArray>();

            List<JObject> allRunners = new List<JObject>();
            foreach (JObject runner in runners)
            {
                allRunners.Add(runner);
            }
            return allRunners;
        }

        public string getVID()
        {
            dynamic payload = new JObject();
            payload["loginEndpoint"] = "https://identitysso.betfair.com.au/api/login";
            payload["loginRedirectUrl"] = "https://www.betfair.com.au/exchange/plus";
            payload["logoutEndpoint"] = "https://identitysso.betfair.com.au/api/logout";
            payload["logoutRedirectUrl"] = "https://www.betfair.com.au/exchange/plus";
            m_httpClient.DefaultRequestHeaders.Remove("Host");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Host", "apieds.betfair.com.au");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json;charset=UTF-8");

            var payloadPost = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            string endpoint = "https://apieds.betfair.com.au/api/eds/ssc/v1?_ak=nzIFcwyWhrlwYMrh";
            HttpResponseMessage ipResponse = m_httpClient.PostAsync(endpoint, payloadPost).Result;
            ipResponse.EnsureSuccessStatusCode();
            string betResult = ipResponse.Content.ReadAsStringAsync().Result;
            string vid = Utils.Between(betResult, "session_id=", "&");
            return vid;
        }

        public bool doLogin()
        {
            try
            {
                m_cookieContainer = new CookieContainer();
                InitHttpClient();

                string mainReferer = m_domain + "/customer/";
                HttpResponseMessage responseMessageLogin = m_httpClient.GetAsync(mainReferer).Result;
                responseMessageLogin.EnsureSuccessStatusCode();

                m_httpClient.DefaultRequestHeaders.Referrer = new Uri(mainReferer);
                dynamic input = new JObject();
                input.username = m_account.b365Username;
                input.password = m_account.b365Password;
                var postData = new StringContent(input.ToString(), Encoding.UTF8, "application/json");

                responseMessageLogin = m_httpClient.PostAsync( m_domain + "/customer/api/login", postData).Result;
                responseMessageLogin.EnsureSuccessStatusCode();



                WriteCookiesToDisk(m_cookieContainer);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected void InitHttpClient()
        {

            HttpClientHandler handler = new HttpClientHandler();

            if (!string.IsNullOrEmpty(Setting.instance.proxy))
            {
                WebProxy proxyItem;
                var useAuth = false;
                proxyItem = new WebProxy(Setting.instance.proxy);
                handler = new HttpClientHandler()
                {
                    Proxy = proxyItem,
                    UseProxy = true,
                    PreAuthenticate = useAuth,
                    UseDefaultCredentials = !useAuth,
                };
            }
            
            handler.CookieContainer = m_cookieContainer;
            m_httpClient = new HttpClient(handler);
            m_httpClient.Timeout = new TimeSpan(0, 0, 100);
            ServicePointManager.DefaultConnectionLimit = 2;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            m_httpClient.DefaultRequestHeaders.ExpectContinue = false;
            ChangeDefaultHeaders();
        }

        protected virtual void ChangeDefaultHeaders()
        {
            m_httpClient.DefaultRequestHeaders.Clear();
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, application/xml; q=0.9, image/webp, */*; q=0.8");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Origin", m_domain);
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
        }

        public void WriteCookiesToDisk(CookieContainer cookieJar)
        {
            using (Stream stream = File.Create(m_account.b365Username + "-cookie.bin"))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, cookieJar);
                }
                catch (Exception e)
                {
                    m_handlerWriteStatus("Problem writing cookies to disk: " + e.GetType());
                }
            }
        }

        public void ReadCookiesFromDisk()
        {
            try
            {
                using (Stream stream = File.Open(m_account.b365Username + "-cookie.bin", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    this.m_cookieContainer = (CookieContainer)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                this.m_cookieContainer = new CookieContainer();
            }
        }

    }
}
