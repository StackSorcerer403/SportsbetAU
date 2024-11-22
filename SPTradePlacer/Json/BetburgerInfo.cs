using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot.Json
{
    [Serializable]
    public class BetburgerInfo
    {
        [JsonIgnore]
        public string formulaKey { get; set; }
        public double value { get; set; }
        [JsonIgnore]
        public string logText { get; set; }
        public double percent { get; set; }
        public double middle_value { get; set; }
        public double ROI { get; set; }
        public string bookmaker { get; set; }
        public string opposite_bookmaker { get; set; }
        public string site { get; set; }
        public string sport { get; set; }
        [JsonIgnore]
        public string sportTitle { get; set; }
        public string homeTeam { get; set; }
        [JsonIgnore]
        public string homeTeamB { get; set; }
        public string awayTeam { get; set; }
        [JsonIgnore]
        public string awayTeamB { get; set; }
        public string eventTitle { get; set; }
        [JsonIgnore]
        public string eventP { get; set; }
        public string eventUrl { get; set; }
        public string outcome { get; set; }
        public double odds { get; set; }
        public double opposite_odds { get; set; }
        public double commission { get; set; }
        public string created { get; set; }
        public string started { get; set; }
        public string updated { get; set; }
        public double stake { get; set; }
        [JsonIgnore]
        public double maxstake { get; set; }
        [JsonIgnore]
        public double minstake { get; set; }
        public string arbId { get; set; }
        public string arbBetId { get; set; }
        public string league { get; set; }
        public string period { get; set; }
        public double profit { get; set; }
        public string direct_link { get; set; }
        public string siteUrl { get; set; }
        public string extra { get; set; }
        public bool isLay { get; set; }
        public double marketDepth { get; set; }
        public DateTime date { get; set; }
        public string current_score { get; set; }

        public bool isLive { get; set; }

        //For Best Odds project
        [JsonIgnore]
        public string source { get; set; }
        [JsonIgnore]
        public string placed { get; set; }

        [JsonIgnore]
        /// <summary>
        ///  0 => Fail 1=> Pending 2=> success
        /// </summary>
        /// 
        public double margin { get; set; }
        [JsonIgnore]
        public string errorMessage { get; set; }
        [JsonIgnore]
        public int uniqueRequestId { get; set; }
        [JsonIgnore]
        public int betId { get; set; }
        public string bookmaker_event_id { get; set; }

        public BetburgerInfo()
        {
            percent = 0;
            margin = 0;
            ROI = 0.0;
            bookmaker = string.Empty;
            sport = string.Empty;
            homeTeam = string.Empty;
            awayTeam = string.Empty;
            homeTeamB = string.Empty;
            awayTeamB = string.Empty;
            eventTitle = string.Empty;
            eventUrl = string.Empty;
            outcome = string.Empty;
            odds = 0.0;
            created = string.Empty;
            started = string.Empty;
            updated = string.Empty;
            arbId = string.Empty;
            arbBetId = string.Empty;
            league = string.Empty;
            period = string.Empty;
            profit = 0.0;
            maxstake = 0;
            stake = 0;
            direct_link = string.Empty;
            siteUrl = string.Empty;
            extra = string.Empty;
            date = DateTime.Now;
            date = date.AddTicks(-(date.Ticks % TimeSpan.TicksPerSecond));
            errorMessage = string.Empty;
            uniqueRequestId = 0;
        }

        public BetburgerInfo(BetburgerInfo info)
        {
            percent = info.percent;
            ROI = info.ROI;
            bookmaker = info.bookmaker;
            sport = info.sport;
            homeTeam = info.homeTeam;
            awayTeam = info.awayTeam;
            eventTitle = info.eventTitle;
            eventUrl = info.eventUrl;
            outcome = info.outcome;
            odds = info.odds;
            created = info.created;
            started = info.started;
            updated = info.updated;
            arbId = info.arbId;
            arbBetId = info.arbBetId;
            league = info.league;
            period = info.period;
            profit = info.profit;
            direct_link = info.direct_link;
            siteUrl = info.siteUrl;
            extra = info.extra;
            date = info.date;
            uniqueRequestId = info.uniqueRequestId;
            source = info.source;
            placed = info.placed;
            isLive = info.isLive;
        }

        public BetburgerInfo clone()
        {
            BetburgerInfo newItem = new BetburgerInfo();
            newItem.formulaKey = this.formulaKey;
            newItem.percent = this.percent;
            newItem.ROI = this.ROI;
            newItem.bookmaker = this.bookmaker;
            newItem.sport = this.sport;
            newItem.sportTitle = this.sportTitle;
            newItem.homeTeam = this.homeTeam;
            newItem.homeTeamB = this.homeTeamB;
            newItem.awayTeam = this.awayTeam;
            newItem.awayTeamB = this.awayTeamB;
            newItem.eventTitle = this.eventTitle;
            newItem.eventP = this.eventP;
            newItem.eventUrl = this.eventUrl;
            newItem.outcome = this.outcome;
            newItem.odds = this.odds;
            newItem.commission = this.commission;

            newItem.created = this.created;
            newItem.started = this.started;
            newItem.updated = this.updated;

            newItem.stake = this.stake;
            newItem.maxstake = this.maxstake;
            newItem.arbId = this.arbId;

            newItem.arbBetId = this.arbBetId;
            newItem.league = this.league;
            newItem.period = this.period;

            newItem.profit = this.profit;
            newItem.direct_link = this.direct_link;
            newItem.siteUrl = this.siteUrl;

            newItem.extra = this.extra;
            newItem.isLay = this.isLay;
            newItem.marketDepth = this.marketDepth;

            newItem.date = this.date;
            newItem.current_score = this.current_score;
            newItem.margin = this.margin;
            newItem.errorMessage = this.errorMessage;
            newItem.uniqueRequestId = this.uniqueRequestId;
            newItem.source = this.source;
            newItem.placed = this.placed;

            newItem.isLive = this.isLive;
            return newItem;
        }

    }
}
