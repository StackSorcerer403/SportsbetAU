using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot.Json
{
    public class JsonRace
    {
        public string raceId { get; set; }
        public string winMarketId { get; set; }
        public string placeMarketId { get; set; }
        public string aVSbMarketId { get; set; }
        public string startTime { get; set; }
        public string meetingId { get; set; }
        public string winMarketName { get; set; }
        public string meetingName { get; set; }
        public bool inplay { get; set; }
        public JsonRace()
        {
            inplay = false;
        }
        public double getRemainSeconds()
        {
            DateTime nowTime = DateTime.UtcNow;
            DateTime raceTime = DateTime.Parse(startTime.ToString());
            double diffSeconds = (raceTime - nowTime).TotalSeconds;
            return diffSeconds;
        }
    }
}
