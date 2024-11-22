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
using BettingBot.Json;
using System.Web;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using AutoIt;
using iTextSharp.text.pdf.parser;
using BettingBot.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static MasterDevs.ChromeDevTools.Protocol.Chrome.ProtocolName;
using BettingBot.Constants;

namespace BettingBot.Controller
{
    public class BookieCtrl
    {
        private WebSocket _webSocket = null;
        public double m_balance;
        public int slip_balance = -1;
        public bool m_isLogged = false;
        public string CSRF = Utils.GetRandomHexNumber(32);
        protected string m_domain = "https://www.orbitxch.com";
        protected CookieContainer m_cookieContainer;
        protected HttpClient m_httpClient = null;
        protected Thread _mainThread = null;
        protected bool _bWorking = false;
        private int _threadCounter = -1;
        private string _token;
        private string appkey;
        private string sessionToken;
        private string _csrfToken;
        public List<string> m_triedBetList = new List<string>();

        private string _waitingRID;
        private string _RespBody;

        private Dictionary<int, string> sportIdDict = new Dictionary<int, string>();
        private JObject marketMatch;
        private Task heartBeatTask;

        private static BookieCtrl _instance = null;
        private bool _isWaiter;
        private JToken _BookmakerOdds;
        private JToken _BetResp;
        private List<RaceItem> _bfRaceList = new List<RaceItem>();
        private int betIndex = 0;
        public List<HorseItem> PlacedBetList = new List<HorseItem>();

        public static BookieCtrl instance
        {
            get
            {
                return _instance;
            }
        }

        public BookieCtrl()
        {
            m_cookieContainer = new CookieContainer();
            ReadCookiesFromDisk();
            InitHttpClient();
            PlacedBetList = IOManager.readBetData();
        }


        static public void CreateInstance()
        {
            _instance = new BookieCtrl();
        }

        public bool doLogin()
        {
            try
            {
                BrowserCtrl.instance.SetTopMost();
                if (doCheckLogged(3) == true)
                {
                    LogMng.instance.PrintLog("Login Success - already!");
                    return true;
                }
                BrowserCtrl.instance.ExecuteScript("document.querySelector(\"button[data-automation-id='header-login-touchable']\").click()");
                Thread.Sleep(3000);
                BrowserCtrl.instance.ExecuteScript("document.querySelector(\"i[data-automation-id='login-username-close']\").click()");
                Thread.Sleep(1000);
                Bitmap source = OcrEngine.instance.CaptureScreen();
                
                Bitmap template = new Bitmap(".\\templates\\login\\username.png");
                Point? pt = GuiAutomation.instance.FindImageOnScreen(source, template);
                AutoIt.AutoItX.MouseClick("LEFT", pt.Value.X, pt.Value.Y, 2, 0);
                // Ctrl + A 
                AutoItX.Send("^a");
                // Delete
                AutoItX.Send("{DEL}");
                AutoItX.Send(Setting.instance.betUser);
                Thread.Sleep(1000);

                //template = new Bitmap(".\\templates\\login\\password.png");
                //pt = GuiAutomation.instance.FindImageOnScreen(source, template);
                ////AutoIt.AutoItX.MouseClick("LEFT", pt.Value.X, pt.Value.Y, 2, 0);
                //AutoIt.AutoItX.MouseClick("LEFT", pt.Value.X, pt.Value.Y, 2, 0);

                AutoIt.AutoItX.Send("{TAB}");
                AutoItX.Send("^a");
                // Delete 
                AutoItX.Send("{DEL}");
                AutoIt.AutoItX.Send(Setting.instance.betPassword.Replace("!", "{!}"));
                Thread.Sleep(1000);

                template = new Bitmap(".\\templates\\login\\btnLogin.png");
                pt = GuiAutomation.instance.FindImageOnScreen(source, template);
                AutoIt.AutoItX.MouseClick("LEFT", pt.Value.X, pt.Value.Y);
                Thread.Sleep(2000);
                
                if (doCheckLogged())
                {
                    LogMng.instance.PrintLog("Login Success!");
                    Thread.Sleep(1000);
                    string accessToken = BrowserCtrl.instance.ExecuteScript("JSON.parse(localStorage.getItem(\"redux-localstorage\")).ui.user.session.accessToken", true);
                    LogMng.instance.PrintLog($"Access Token = {accessToken}");
                    Setting.instance.fingerPrint = accessToken;
                    Setting.instance.saveSettingInfo();
                    return true;
                }
            }
            catch(Exception ex)
            {
                LogMng.instance.PrintLog("Exception in doLogin in " + ex.ToString());
            }
            return false;
        }

        public double getBalance()
        {
            double balance = -1;
            try
            {
                if (string.IsNullOrEmpty(Setting.instance.fingerPrint)) return balance;
                string url = "https://www.sportsbet.com.au/apigw/accounts/balance?pendingbetcount=true&freebetcount=true&jointAccountBalance=true";
                m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accesstoken", Setting.instance.fingerPrint);
                HttpResponseMessage httpResp = m_httpClient.GetAsync(url).Result;
                httpResp.EnsureSuccessStatusCode();
                string strResp = httpResp.Content.ReadAsStringAsync().Result;
                balance = Utils.ParseToDouble(JObject.Parse(strResp)["balance"].ToString());
            }
            catch
            {
            }
            return balance;
        }

        public bool doCheckLogged(int delay = 10)
        {
            try
            {
                int retryCounter = 10 * delay;
                string strRes = BrowserCtrl.instance.ExecuteScript("document.querySelector('span[data-automation-id=\"header-account-balance\"]') !== null?true: false", true);
                while (strRes != "True" && --retryCounter > 0)
                {
                    Thread.Sleep(100);
                }
                if (strRes == "False") return false;
                string curBalance = BrowserCtrl.instance.ExecuteScript("document.querySelector('span[data-automation-id=\"header-account-balance\"]').innerText", true);
                LogMng.instance.PrintLog($"Current balance={curBalance}");
                m_balance = Utils.ParseToDouble(curBalance);
                string accessToken = BrowserCtrl.instance.ExecuteScript("JSON.parse(localStorage.getItem(\"redux-localstorage\")).ui.user.session.accessToken", true);
                LogMng.instance.PrintLog($"Access Token = {accessToken}");
                Setting.instance.fingerPrint = accessToken;
                Setting.instance.saveSettingInfo();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public void startAutoStaker()
        {
            try
            {                
                LogMng.instance.PrintLog($"Auto Staker Bot has been started!");

                // check betslip empty, if there is even one, print log betslip balance
                string slipBalance = BrowserCtrl.instance.ExecuteScript("document.querySelector('span[data-automation-id=\"header-bet-count\"]').innerText", true);
                if (slip_balance != int.Parse(slipBalance))
                {
                    LogMng.instance.PrintLog($"Betslip Balance={slipBalance}");
                    slip_balance = int.Parse(slipBalance);                    
                }
                
                // check if betslip window is closed - in case of yes click betslip button
                string btnRes = BrowserCtrl.instance.ExecuteScript("document.querySelector('span[data-automation-id=\"BetslipHeader\"]') !== null?true: false", true);
                if (btnRes == "False")
                {
                    Thread.Sleep(5000);
                    BrowserCtrl.instance.ExecuteScript("document.querySelector(\"button[data-automation-id='header-betslip-touchable']\").click()");
                }

                // autofill to all stake in betslip
                if (slip_balance != 0)
                {
                    autofillBetStake();
                }
            }
            catch (Exception ex)
            {
                LogMng.instance.PrintLog("Exception in AutoStaker " + ex.ToString());
            }
        }

        public void autofillBetStake()
        {
            try
            {                
                // factors of stack formula
                double odds, stack, totalReturn = Setting.instance.totalReturn;
                int i = 0;
                while (i < slip_balance)
                {
                    BrowserCtrl.instance.ExecuteScript($@" document.querySelectorAll('[data-automation-id^=""betslip-single-""] [data-automation-id=""betslip-stake""]')[{i}].focus();");
                    BrowserCtrl.instance.ExecuteScript($@"document.querySelectorAll('[data-automation-id^=""betslip-single-""] [data-automation-id=""betslip-stake""]')[{i}].value = '';", true);
                    odds = Convert.ToDouble(BrowserCtrl.instance.ExecuteScript($@" document.querySelectorAll('[data-automation-id^=""betslip-single-""] [data-automation-id^=""betslip-bet-odds""]')[{i}].innerText", true));
                    stack = getBetStake(odds, totalReturn);
                    AutoItX.Send("^a");
                    AutoItX.Send("{DEL}");
                    AutoItX.Send("$" + stack.ToString());
                    i++;
                    Thread.Sleep(3000);
                }
            }
            catch
            { 
            }
        }

        public double getBetStake(double odds, double totalReturn)
        {
            return Math.Round(totalReturn / (odds - 1), 2);
        }

        public void startAutoSlip()
        {
            try
            {
                LogMng.instance.PrintLog($"Auto Slip Bot has been started!");
            }
            catch
            {

            }
        }

        public List<RaceItem> GetBfRaceList(bool bForce = false)
        {
            List<RaceItem> raceList = new List<RaceItem>();
            try
            {

                if (string.IsNullOrEmpty(Setting.instance.EXData)) return raceList;
                JArray marketList = JsonConvert.DeserializeObject<JArray>(Setting.instance.EXData);
                foreach (var marketNode in marketList)
                {
                    string countryCode = marketNode["event"]["countryCode"].ToString();
                    if (!countryCode.Equals("AU") && !countryCode.Contains("NZ"))
                        continue;
                    RaceItem newItem = new RaceItem();
                    newItem.trackTitle = marketNode["event"]["venue"].ToString();
                    newItem.type = marketNode["type"].ToString();
                    
                    if (!Setting.instance.enableHorse && newItem.type == "horse") continue;
                    if (!Setting.instance.enableDog && newItem.type == "greyhound") continue;
                    if (!Setting.instance.enableHarness && newItem.type == "harness") continue;

                    newItem.venue = marketNode["event"]["venue"].ToString();
                    newItem.directLink = marketNode["marketId"].ToString() + "|" + marketNode["event"]["id"].ToString();
                    newItem.raceTitle = marketNode["event"]["name"].ToString();
                    newItem.marketName = marketNode["marketName"].ToString();
                    newItem.winMarketId = marketNode["marketId"].ToString();
                    newItem.raceId = marketNode["event"]["id"].ToString();
                    newItem.raceStart = DateTime.Parse(marketNode["marketStartTime"].ToString());
                    //newItem.horseList = GetHorseList(newItem.winMarketId);
                    raceList.Add(newItem);
                }
            }
            catch (Exception ex)
            {
            }
            _bfRaceList = raceList;
            return raceList;
        }
        private bool isSameRace(string b365Track, string bfTrack)
        {
            b365Track = b365Track.ToLower();
            bfTrack = bfTrack.ToLower();
            //string trackPrefix = bfTrack.Split(' ')[0];
            if (!bfTrack.Contains(b365Track))
            {
                if (b365Track.Contains("Mount Barker".ToLower()) && bfTrack.Contains("MtBk".ToLower()))
                    return true;

                if (b365Track.Contains("Murray Bridge".ToLower()) && bfTrack.Contains("MBdg".ToLower()))
                    return true;

                if (b365Track.Contains("Devonport".ToLower()) && bfTrack.Contains("Devn".ToLower()))
                    return true;

                if (b365Track.Contains("Sunshine Coast".ToLower()) && bfTrack.Contains("SCst".ToLower()))
                    return true;

                if (b365Track.Contains("Port Lincoln".ToLower()) && bfTrack.Contains("PLin".ToLower()))
                    return true;

                if (b365Track.Contains("Alice Springs".ToLower()) && bfTrack.Contains("ASpr".ToLower()))
                    return true;

                if (b365Track.Contains("Gold Coast".ToLower()) && bfTrack.Contains("GCst".ToLower()))
                    return true;

                if (b365Track.Contains("Arawa Park".ToLower()) && bfTrack.Contains("Roto".ToLower()))
                    return true;

                if (b365Track.Contains("Albion Park".ToLower()) && bfTrack.Contains("APrk".ToLower()))
                    return true;
                if (b365Track.Contains("Ascot".ToLower()) && bfTrack.Contains("Ascot".ToLower()))
                    return true;
                if (b365Track.Contains("Eagle Farm".ToLower()) && bfTrack.Contains("EFrm".ToLower()))
                    return true;
                if (b365Track.Contains("Kembla Grange".ToLower()) && bfTrack.Contains("KemG".ToLower()))
                    return true;
                if (b365Track.Contains("Te Rapa".ToLower()) && bfTrack.Contains("TRap".ToLower()))
                    return true;
                if (b365Track.Contains("Ascot Park".ToLower()) && bfTrack.Contains("AsPk".ToLower()))
                    return true;
                if (b365Track.Contains("Gloucester Park".ToLower()) && bfTrack.Contains("GlPk".ToLower()))
                    return true;
                if (b365Track.Contains("Globe Derby".ToLower()) && bfTrack.Contains("GlbD".ToLower()))
                    return true;

                if (b365Track.Contains("Horsham".ToLower()) && bfTrack.Contains("Hshm".ToLower()))
                    return true;

                if (b365Track.Contains("Warragul".ToLower()) && bfTrack.Contains("Wgul".ToLower()))
                    return true;

                return false;
            }
            return true;
        }
        public int Fill365DirectLink(List<RaceItem> bookieRaceList)
        {
            int foundCounter = 0;
            try
            {
                foreach (RaceItem bfRace in _bfRaceList)
                {
                    foreach (RaceItem b365Race in bookieRaceList)
                    {

                        
                        if (b365Race.type != bfRace.type && b365Race.type == "greyhound") continue;
                        if (!isSameRace(b365Race.trackTitle, bfRace.trackTitle))
                        {
                            if (!b365Race.trackTitle.ToLower().Contains(bfRace.venue.ToLower())) continue;
                        }
                        string tabRaceNumber = b365Race.raceTitle.Split(' ')[0];
                        if (!bfRace.marketName.StartsWith(tabRaceNumber)) continue;
                        //LogMng.instance.PrintLog(string.Format("{0}-tab = {1}=bf | {2} seconds", b365Race.raceTitle, bfRace.trackTitle, bfRace.GetLeftSeconds()));
                        bfRace.type = b365Race.type;
                        bfRace.b365DirectLink = b365Race.directLink;
                        //bfRace.raceStart = b365Race.raceStart;
                        foundCounter++;
                        break;
                    }
                    if (!string.IsNullOrEmpty(bfRace.b365DirectLink))
                    {
                        //LogMng.instance.PrintLog($"{bfRace.raceTitle} {bfRace.raceStart} {bfRace.GetLeftSeconds()}");
                    }
                }
            }
            catch
            {

            }
            return foundCounter;
        }

        public List<HorseItem> GetBfHorseList(string directLink)
        {
            List<HorseItem> horseList = new List<HorseItem>();
            try
            {
                if (string.IsNullOrEmpty(Setting.instance.EXData)) return horseList;
                JArray marketList = JsonConvert.DeserializeObject<JArray>(Setting.instance.EXData);
                foreach (var marketNode in marketList)
                {
                    if (!marketNode["marketId"].ToString().Equals(directLink)) continue;
                    if (marketNode["status"] != null && marketNode["status"].ToString() != "OPEN")
                        continue;
                    JArray runners = marketNode["runners"].ToObject<JArray>();

                    foreach (JObject runner in runners)
                    {
                        string runnerName = runner["runnerName"].ToString().Trim();
                        int runnerNo = int.Parse(runner["sortPriority"].ToString());

                        string runnerTitle = runnerName.Split('.')[1];
                        HorseItem newItem = new HorseItem();
                        //m_handlerWriteStatus(" - " + runnerName);
                        newItem.title = runnerTitle;
                        newItem.no = runnerNo;
                        newItem.marketId = directLink;
                        newItem.selectionId = runner["selectionId"].ToString();
                        if (runner["ex"] == null) continue;
                        if (runner["ex"]["availableToBack"] == null) continue;
                        try
                        {
                            double backOdds = Utils.ParseToDouble(runner["ex"]["availableToBack"][0]["price"].ToString());
                            double layOdds = Utils.ParseToDouble(runner["ex"]["availableToLay"][0]["price"].ToString());
                            newItem.odds = (layOdds + layOdds) / 2;
                            newItem.backOdds = backOdds;
                            newItem.layOdds = layOdds;
                        }
                        catch
                        {
                        }
                        horseList.Add(newItem);
                    }
                }
            }
            catch (Exception ex)
            {
                //m_handlerWriteStatus("Exception in GetHorseList: " + ex.ToString());
            }
            return horseList;
        }

        public List<HorseItem> GetHorseList(string directLink)
        {
            List<HorseItem> horseList = new List<HorseItem>();
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage allRacesResp = httpClient.GetAsync(directLink).Result;
                string racesStr = allRacesResp.Content.ReadAsStringAsync().Result;
                dynamic jsonMessage = JsonConvert.DeserializeObject<dynamic>(racesStr);
                foreach (dynamic itemRunner in jsonMessage.markets[0].selections)
                {
                    try
                    {
                        HorseItem newItem = new HorseItem();
                        newItem.no = int.Parse(itemRunner.runnerNumber.ToString());
                        newItem.outcomeId = itemRunner.id.ToString();
                        foreach (var price in itemRunner.prices)
                        {
                            if (price["priceCode"].ToString() == "L")
                            {
                                newItem.odds = Utils.ParseToDouble(price.winPrice.ToString());
                                newItem.priceData = price.ToString();
                            }
                        }
                        newItem.directLink = directLink;
                        newItem.title = itemRunner.name.ToString(); ;
                        horseList.Add(newItem);
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                LogMng.instance.PrintLog(ex.ToString());
            }
            return horseList;
        }

        public List<RaceItem> getNextRaces(double beforeRaceOff)
        {

            List<RaceItem> nextRaceList = new List<RaceItem>();
            try
            {
                foreach (RaceItem raceItem in _bfRaceList)
                {
                    if (!Setting.instance.enableHorse && raceItem.type == "horse") continue;
                    if (!Setting.instance.enableDog && raceItem.type == "greyhound") continue;
                    if (!Setting.instance.enableHarness && raceItem.type == "harness") continue;

                    double leftSeconds = raceItem.GetLeftSeconds();
                    if (leftSeconds <= beforeRaceOff && leftSeconds > - 60* 8)
                    {
                        nextRaceList.Add(raceItem);
                        //LogMng.instance.PrintLog(string.Format("betfair => {0} {1}| {2} seconds left", raceItem.raceTitle, raceItem.marketName, leftSeconds));
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return nextRaceList;
        }
        public List<RaceItem> GetRaceList(bool bForce = false)
        {
            List<RaceItem> raceList = new List<RaceItem>();
            try
            {
                DateTime nowDate = DateTime.Now;
                string strToday = nowDate.ToString("yyyy-MM-dd");
                JArray jArray = new JArray();
                string allRacesStr = string.Empty;

                string urlForRaces = string.Format("https://www.sportsbet.com.au/apigw/sportsbook-racing/Sportsbook/Racing/AllRacing/{0}", strToday);
                LogMng.instance.PrintLog(urlForRaces);
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage allRacesResp = httpClient.GetAsync(urlForRaces).Result;
                allRacesStr = allRacesResp.Content.ReadAsStringAsync().Result;
                dynamic jsonMessage = JsonConvert.DeserializeObject<dynamic>(allRacesStr);
                // Horse Racing
                for(int i = 0; i < 3; i++)
                {
                    var selection = jsonMessage.dates[0]["sections"][i];
                    foreach (dynamic meeting in selection["meetings"])
                    {
                        foreach (dynamic race in meeting.events)
                        {
                            try
                            {
                                RaceItem newItem = new RaceItem();
                                newItem.type = selection["raceType"].ToString();
                                if (!Setting.instance.enableHorse && newItem.type == "horse") continue;
                                if (!Setting.instance.enableDog && newItem.type == "greyhound") continue;
                                if (!Setting.instance.enableHarness && newItem.type == "harness") continue;
                                newItem.raceStart = DateTimeOffset.FromUnixTimeSeconds(long.Parse(race.startTime.ToString())).LocalDateTime;
                                newItem.trackTitle = meeting.name.ToString();
                                newItem.directLink = $"https://www.sportsbet.com.au/apigw/sportsbook-racing/{race.httpLink.ToString()}";
                                newItem.raceTitle = race.name.ToString();
                                //LogMng.instance.PrintLog("sportbet.au => " + newItem.raceTitle);
                                raceList.Add(newItem);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                
                string strContent = jArray.ToString();
                strContent.Replace("\r\n", "");
            }
            catch (Exception ex)
            {
                LogMng.instance.PrintLog("Exception in GetUKHorseList: " + ex.ToString());
            }
            return raceList;
        }

        public bool startNewTrade(HorseItem betItem)
        {
            try
            {
                if (betItem.odds < betItem.layOdds || betItem.layOdds == 0) return false;

                double percent = 100 * ((betItem.odds - betItem.layOdds) / betItem.layOdds);
                
                if (percent < Setting.instance.minPercent) return false;
                if (percent > Setting.instance.maxPercent) return false;
                if (betItem.odds < Setting.instance.minOdds) return false;
                if (betItem.odds > Setting.instance.maxOdds) return false;

                if (m_triedBetList.Contains(betItem.outcomeId)) return false;
                //Place Bet
                placeBet:
                LogMng.instance.PrintLog($"{betItem.no} {betItem.title} odds:{betItem.odds} bf odds:{betItem.layOdds} percent:{percent}");
                string randomString = Guid.NewGuid().ToString("N").Substring(0, 29);
                string url = "https://www.sportsbet.com.au/apigw/acs/bets";
                m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accesstoken", Setting.instance.fingerPrint);
                m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("transuniqueid", randomString);
                string strData = @"
                {
                    ""betItems"": [
                        {
                            ""betNo"": 6,
                            ""stakePerLine"": 0.1,
                            ""numLines"": 1,
                            ""betType"": ""SGL"",
                            ""legs"": [
                                {
                                    ""legNo"": 6,
                                    ""legSort"": ""--"",
                                    ""legType"": ""W"",
                                    ""legDesc"": """",
                                    ""isPYPSingle"": false,
                                    ""parts"": [
                                        {
                                            ""outcome"": 1002323685,
                                            ""partNo"": 1,
                                            ""priceType"": ""L"",
                                            ""partDesc"": ""RACECARD"",
                                            ""priceNum"": 9,
                                            ""priceDen"": 2
                                        }
                                    ]
                                }
                            ],
                            ""legType"": ""W""
                        }
                    ],
                    ""checkBalance"": true,
                    ""errorDetail"": ""ALL"",
                    ""firstBet"": true,
                    ""fullDetails"": true,
                    ""pendingBetCount"": true,
                    ""returnBalance"": true,
                    ""returnCashoutAvailable"": true
                }";
                JObject jsonData = JObject.Parse(strData);
                betIndex++;
                jsonData["betItems"][0]["betNo"] = betIndex;
                jsonData["betItems"][0]["stakePerLine"] = Setting.instance.flatStake;
                jsonData["betItems"][0]["legs"][0]["legNo"] = betIndex;
                jsonData["betItems"][0]["legs"][0]["parts"][0]["outcome"] = betItem.outcomeId;
                jsonData["betItems"][0]["legs"][0]["parts"][0]["priceNum"] = JObject.Parse(betItem.priceData)["winPriceNum"];
                jsonData["betItems"][0]["legs"][0]["parts"][0]["priceDen"] = JObject.Parse(betItem.priceData)["winPriceDen"];
                StringContent payload = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResp = m_httpClient.PostAsync(url, payload).Result;
                string strResp = httpResp.Content.ReadAsStringAsync().Result;
                if(httpResp.StatusCode == HttpStatusCode.Unauthorized)
                {
                    doLogin();
                    goto placeBet;
                }
                httpResp.EnsureSuccessStatusCode();
                JObject jsonResp = JObject.Parse(strResp);
                if (jsonResp["betPlacements"] != null && jsonResp["betPlacements"].Count() > 0)
                {
                    m_triedBetList.Add(betItem.outcomeId);
                    betItem.stake = Setting.instance.flatStake;
                    betItem.percent = percent;
                    PlacedBetList.Add(betItem);
                    LogMng.instance.PrintLog($"Bet Success | balance ={m_balance}$");
                    IOManager.saveBetData(PlacedBetList);
                    m_balance = Utils.ParseToDouble(jsonResp["accountBalance"]["balance"].ToString());
                    
                    TelegramBot.sendMessage($"[{betItem.type}]{betItem.raceName} {betItem.beforeSeconds} Seconds \n" +
                        $"{betItem.no}.{betItem.title} Bookie: {betItem.odds} Betfair: {betItem.layOdds} ({percent}%)\n" +
                        $"Stake ${betItem.stake} Balance:{m_balance}");
                }
                else
                {
                    LogMng.instance.PrintLog(strResp);
                }
            }
            catch(Exception ex)
            {
                LogMng.instance.PrintLog("Exception in startNewTrade in " + ex.ToString());
            }
            return false;
        }

        public bool betRace(string matchId, string marketId, double odds)
        {
            try
            {
                if (!doCheckLogged(1)) BookieCtrl.instance.doLogin();
                string strStake = (Setting.instance.flatStake * 100).ToString();
                string strScript = "{\"T\":1,\"A\":" + strStake +  ",\"B\":[{\"BID\":" + marketId + ",\"O\":" + (odds* 1000).ToString() + "}],\"CV\":\"2.109.2-desktop\",\"locale\":\"es\"}";
                BrowserCtrl.instance.ExecuteScript(File.ReadAllText("inject.txt"));
                BrowserCtrl.instance.ExecuteScript($"sendPlaceBet('{strScript}')");
                _BetResp = null;
                int retryCounter = 10 * 4;
                while(_BetResp == null || --retryCounter > 0)
                {
                    Thread.Sleep(100);
                }
                LogMng.instance.PrintLog(_BetResp.ToString());
                if (_BetResp["BSID"] != null && !string.IsNullOrEmpty(_BetResp["BSID"].ToString()) && _BetResp["ESTR"] != null && _BetResp["ESTR"].ToString() == "OK")
                {
                    LogMng.instance.PrintLog(string.Format("*** SUCCESS BET ***"));
                    return true;
                }
                LogMng.instance.PrintLog(string.Format("*** FAILED BET ***"));
                return false;
            }
            catch (Exception ex)
            {
                LogMng.instance.PrintLog(_RespBody);
                LogMng.instance.PrintLog("Exception in placeBet : " + ex.ToString());
            }
            return true;
        }

        public void checkIP()
        {
            HttpResponseMessage respMessage =  m_httpClient.GetAsync("http://lumtest.com/myip.json").Result;
            respMessage.EnsureSuccessStatusCode();
            string strMessage = respMessage.Content.ReadAsStringAsync().Result;
            LogMng.instance.PrintLog(strMessage);
        }

        public string getIP()
        {
            HttpResponseMessage respMessage = m_httpClient.GetAsync("http://lumtest.com/myip.json").Result;
            respMessage.EnsureSuccessStatusCode();
            string strMessage = respMessage.Content.ReadAsStringAsync().Result;
            return JObject.Parse(strMessage)["ip"].ToString();
        }


        protected void InitHttpClient()
        {

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            if (!string.IsNullOrEmpty(Setting.instance.proxy) && !string.IsNullOrEmpty(Setting.instance.proxyUser) && !string.IsNullOrEmpty(Setting.instance.proxyPass))
            {
                var useAuth = true;
                var credentials = new NetworkCredential(Setting.instance.proxyUser, Setting.instance.proxyPass);
                WebProxy proxyItem = new WebProxy(Setting.instance.proxy);
                proxyItem.Credentials = credentials;
                handler = new HttpClientHandler()
                {
                    Proxy = proxyItem,
                    UseProxy = true,
                    PreAuthenticate = useAuth,
                    UseDefaultCredentials = !useAuth,
                };
            }else if (!string.IsNullOrEmpty(Setting.instance.proxy))
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
            m_cookieContainer.Add(new Uri(m_domain), new Cookie("CSRF-TOKEN", CSRF));
        }

        protected virtual void ChangeDefaultHeaders()
        {
            m_httpClient.DefaultRequestHeaders.Clear();
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, application/xml; q=0.9, image/webp, */*; q=0.8");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-requested-with", "XMLHttpRequest");
        }

        protected void getHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            handler.CookieContainer = m_cookieContainer;
            m_httpClient = new HttpClient(handler);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            m_httpClient.DefaultRequestHeaders.ExpectContinue = false;
        }

        public void WriteCookiesToDisk(CookieContainer cookieJar)
        {

        }

        public void checkHistory()
        {
            try
            {
                HttpResponseMessage response = m_httpClient.GetAsync($"https://sports-api.cloudbet.com/pub/v4/bets/history?limit=1000&offset=0").Result;
                response.EnsureSuccessStatusCode();
                string strContent = response.Content.ReadAsStringAsync().Result;
                JObject obj = JsonConvert.DeserializeObject<JObject>(strContent);
                LogMng.instance.PrintLog(obj.ToString());
            }
            catch
            {

            }
        }

        public void ReadCookiesFromDisk()
        {
           
        }
    }
}
