using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BettingBot.Controller
{
    public class TelegramBot
    {

        static List<JObject> lastMessageList = new List<JObject>();
        static List<JObject> lastPickList = new List<JObject>();
        public TelegramBot()
        {
        }
        
        public static bool isNewData(string logKey, ref bool bReversed)
        {
            JObject marketResult = JsonConvert.DeserializeObject<JObject>(logKey);
            double curValue = Utils.ParseToDouble(marketResult.SelectToken("DiffOdds").ToString());
            double curOdds = Utils.ParseToDouble(marketResult.SelectToken("Odds").ToString());
            string curSelectionId = marketResult.SelectToken("SelectionId").ToString();
            string curMarketId = marketResult.SelectToken("MarketId").ToString();

            for (int i = 0; i < lastMessageList.Count; i++)
            {
                var item = lastMessageList[lastMessageList.Count - i - 1];
                double value = Utils.ParseToDouble(item.SelectToken("DiffOdds").ToString());
                double odds = Utils.ParseToDouble(item.SelectToken("Odds").ToString());
                string SelectionId = item.SelectToken("SelectionId").ToString();
                string MarketId = item.SelectToken("MarketId").ToString();
                if (SelectionId.Trim().Equals(curSelectionId.Trim()))
                {
                     return false;
                }
            }

            if (lastMessageList.Count > 50)
            {
                lastMessageList = lastMessageList.GetRange(20, lastMessageList.Count - 20);
            }

            lastMessageList.Add(marketResult);
            return true;
        }

        public static void sendMessage(string message)
        {
            TelegramBotClient botClient = new TelegramBotClient("704271441:AAG-Z5oqj36ERnEPyKz2ODr5yyQZhy6DlZw");
            botClient.SendTextMessageAsync("-1002470768073", message);
        }
    }
}
