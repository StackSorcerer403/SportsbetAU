using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot.Json
{
    [Serializable]
    public enum ODDSTYPES
    {
        threeway = 69,
        points = 192,
        totals = 47,
        ahc = 48,
        dnb = 112,
        ehc = 8,
        setHandicap = 71,
        setOverUnder = 100,
        gameHandicap = 39,
        doubble = 9,
        homeAway = 70,
        moneyline = 466,
        killsHandicap = 365,
        killsOverUnder = 328,
        roundsHandicap = 379,
        roundsOverUnder = 377
    };
    [Serializable]
    public class JsonTrade
    {
        public string bet365Link { get; set; }
        public string selectionId { get; set; }
        public string id { get; set; }
        public string homeTeam { get; set; }
        public string homeTeamId { get; set; }
        public string awayTeam { get; set; }
        public string awayTeamId { get; set; }
        public string countryCodeHome { get; set; }
        public string countryCodeAway { get; set; }
        public string bookmaker { get; set; }
        public string couponKey { get; set; }
        public string countryName { get; set; }
        public string eventId { get; set; }
        public int eventPartId { get; set; }
        public double edge { get; set; }
        public double kelly { get; set; }
        public string leagueName { get; set; }
        public double yardstick { get; set; }
        public double volume { get; set; }
        public int outcomeTypeId { get; set; }
        public int oddsType { get; set; }
        public double oddsTypeCondition { get; set; }
        public double baseline { get; set; }
        public double odds { get; set; }
        public string lastUpdated { get; set; }
        public string output { get; set; }
        public string sportId { get; set; }
        public string startTime { get; set; }
        public string templateId { get; set; }
        public int typeId { get; set; }
        public int venueId { get; set; }
        public string bettingOfferId { get; set; }
        public string lineId { get; set; }
        public string outcomeId { get; set; }
        public string market { get; set; }
        public string b3Market { get; set; }
        public string period { get; set; }
        public string bet365FI { get; set; }
        public string bookmakerName { get; set; }
        public string sportName { get; set; }
        public string startIn { get; set; }
        public double beforeKickOff { get; set; }
        public string participant { get; set; }
        public string outcomeText { get; set; }
        public string marketText { get; set; }
        public string runnerTextAlt
        {
            get
            {
                string participant = "o1" == this.output ? this.homeTeamId : this.awayTeamId;
                switch ((ODDSTYPES)oddsType)
                {
                    case ODDSTYPES.threeway:
                        {
                            participant = "o1" == this.output ? this.homeTeamId : this.output == "o3" ? this.awayTeamId : "";
                            return participant == homeTeamId ? "Home" : participant == awayTeamId ? "Away" : "X";
                        }
                    case ODDSTYPES.roundsOverUnder:
                        return "o1" == output ? $"Over" : "o2" == output ? $"Under" : "N/A";
                    case ODDSTYPES.killsOverUnder:
                        return "o1" == output ? $"Over" : "o2" == output ? $"Under" : "N/A";
                    case ODDSTYPES.setOverUnder:
                        return "o1" == output ? $"Over" : "o2" == output ? $"Under" : "N/A";
                    case ODDSTYPES.totals:
                        return "o1" == output ? $"Over" : "o2" == output ? $"Under" : "N/A";
                    case ODDSTYPES.homeAway:
                        return participant == homeTeamId ? "Home" : "Away";
                    case ODDSTYPES.moneyline:
                        return participant == homeTeamId ? "Home" : "Away";
                    case ODDSTYPES.dnb:
                        return participant == homeTeamId ? "Home" : "Away";
                    case ODDSTYPES.ahc:
                        return participant == homeTeamId ? $"Home" : $"Away";
                    case ODDSTYPES.points:
                        return "o1" == output ? $"Home" : $"Away";
                    case ODDSTYPES.ehc:
                        return "o1" == output ? $"Home" : $"Away";
                    case ODDSTYPES.setHandicap:
                    case ODDSTYPES.gameHandicap:
                        return "o1" == output ? $"Home" : $"Away";
                    case ODDSTYPES.roundsHandicap:
                        return participant == homeTeamId ? $"Home" : $"Away";
                    case ODDSTYPES.killsHandicap:
                        return participant == homeTeamId ? $"Home" : $"Away";
                    default:
                        return "N/A";
                }
                return string.Empty;
            }

            set { }
        }

        public string runnerText { get; set; }
        public DateTime hiddenTime { get; internal set; }
        public double stake { get; internal set; }
        public string vbMarket { get; internal set; }

        public JsonTrade()
        {
        }
    }
}
