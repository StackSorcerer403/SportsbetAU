using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot.Json
{
    [Serializable]
    public class RaceItem
    {
        public string type { get; set; }
        public string trackTitle { get; set; }
        public string raceTitle { get; set; }
        public string venue { get; set; }
        public DateTime raceStart { get; set; }
        public string directLink { get; set; }
        public string b365DirectLink { get; set; }
        public string winMarketId { get; set; }
        public string placeMarketId { get; set; }
        public string raceId { get; set; }

        public List<HorseItem> horseList { get; set; }
        public string marketName { get; set; }

        public double GetLeftSeconds()
        {
            DateTime nowTime = DateTime.UtcNow;
            DateTime raceTime = raceStart;
            double diffSeconds = (raceTime - nowTime).TotalSeconds;
            return diffSeconds;
        }

        public long getTimeStamp()
        {
            TimeSpan t = (raceStart - new DateTime(1970, 1, 1));
            long timestamp = (long)t.TotalMilliseconds;
            return timestamp;
        }

        public string GetJsonContent()
        {
            dynamic content = new JObject();
            content.raceTitle = raceTitle;
            content.horses = new JArray();
            if (horseList != null)
            {
                foreach (HorseItem item in horseList)
                {
                    content.horses.Add(item.title);
                }
            }
            return content.ToString();
        }
    }
}
