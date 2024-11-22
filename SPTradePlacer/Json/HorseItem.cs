using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BettingBot.Json
{
    [Serializable]
    public class HorseItem
    {
        public int no { get; set; }
        public DateTime raceStart { get; set; }
        public string raceName { get; set; }
        public double percent { get; set; }
        public string title { get; set; }
        public string title2 { get; set; }
        public double odds { get; set; }
        public double tabOdds { get; set; }
        public double backOdds { get; set; }
        public double layOdds { get; set; }
        public string bs { get; set; }
        public string marketId { get; set; }
        public string selectionId { get; set; }
        public string directLink { get; set; }
        public string outcomeId { get; set; }

        [JsonIgnore]
        public string priceData { get; set; }
        public long foundTime { get; set; }

        public double beforeSeconds
        {
            get
            {
                DateTime nowTime = DateTime.UtcNow;
                DateTime raceTime = raceStart;
                double diffSeconds = (raceTime - nowTime).TotalSeconds;
                return diffSeconds;
            }
        }

        public double stake { get; internal set; }
        public string type { get; internal set; }
    }
}
