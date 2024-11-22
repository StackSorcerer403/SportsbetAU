using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipsterSW.Constant;

namespace BettingBot.Json
{
    public class VBetGame
    {
        public string sport;
        public string region;
        public int leagueId { get; internal set; }
        public int type { get; set; }
        public long start_ts { get; set; }
        public string team1_name { get; set; }
        public string team2_name { get; set; }
        public string id { get; set; }
        public int is_live { get; set; }

        public double Distance(string home, string away)
        {

            double homeDist = JaroWinklerDistance.distance(team1_name, home);
            double awayDist = JaroWinklerDistance.distance(team2_name, away);
            return (homeDist + awayDist) / 2;
        }
    }
}
