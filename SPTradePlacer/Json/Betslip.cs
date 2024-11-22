using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot.Json
{
    public enum CURRENT_STATUS 
    {
        CREATED,
        ADDED,
        CLOSED,
        PLACED,
    }
    public class Betslip
    {
        public string eventId { get; set; }
        public string marketId { get; set; }
        public string betslipId { get; set; }
        public string match { get; set; }
        public string league { get; set; }
        public string marketDescription { get; set; }
        public double stake { get; set; }
        public Dictionary<string, double> oddsDict { get; set; }
        public Dictionary<string, string> accountsDict { get; set; }
        public CURRENT_STATUS curStatus { get; set; }
        public DateTime created { get; set; }
        public DateTime added { get; set; }
        public DateTime cancelled { get; set; }
        public string oppositeKey { get; set; }
        public string Key { get; set; }

        public DateTime startTime { get; set; }

        public Betslip()
        {
            oddsDict = new Dictionary<string, double>();
            accountsDict = new Dictionary<string, string>();
            curStatus = CURRENT_STATUS.CREATED;
            created = DateTime.Now;
        }

        public double GetPinPrice()
        {
            lock (oddsDict)
            {
                foreach (var key in oddsDict.Keys)
                {
                    if (key.ToLower().Contains("pin"))
                    {
                        return oddsDict[key];
                    }
                }
            }
            return -1;
        }

        public double GetMaxPrice(ref string softBook)
        {
            double maxOdds = -1;
            foreach (var key in oddsDict.Keys)
            {
                if (!key.ToLower().Contains("pin"))
                {
                    if(maxOdds < oddsDict[key])
                    {
                        maxOdds = oddsDict[key];
                        softBook = key;
                    }
                }
            }
            return maxOdds;
        }

        public double GetValue(ref string softBook,ref double softOdds)
        {
            return 100;
        }
        public bool IsExpired()
        {
            double leftMins = (startTime - DateTime.Now).TotalMinutes;
            if (leftMins <= 2)
                return true;
            return false;
        }

        public static int GetCountByStatus(List<Betslip> list, CURRENT_STATUS status) 
        {
            int count = 0;
            foreach(var item in list)
            {
                if (item.curStatus == status)
                    count++;
            }
            return count;
        }
    }
}
