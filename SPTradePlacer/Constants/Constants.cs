using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot.Constants
{
    public enum State
    {
        Init,
        Running,
        Pause,
        Stop
    }

    public enum VALIDATE_CODE
    {
        SUCCESS,
        NOEXIST_KEY,
        INUSE_KEY,
        PAUSED_KEY,
        NEW_KEY,
        INVALID
    }

    public enum ValidationState
    {
        WITHOUT_KEY,
        SUCCESS,
        FAILURE,
    }
    public enum SOURCE
    {
        TIPSTER,
        COPYBET,
        BETBURGER,
        DOMBETTING,
        BFLIVE,
        BASHING,
        USAHORSE,
        DOG_DOG,
        AUS_HH,
        DOGWIN,
        RACING_INVEST,
        DOG_PREMATCH,
    }

    public class GlobalConstants
    {
        public static State state = State.Init;
        public static ValidationState validationState;

    }

    public static class Constants
    {
        public static string formatStringBet = "\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"";
        public static string headerStringBet = "\"Time\",\"League\",\"Event\",\"Outcome\",\"Value(%)\",\"Odds\",\"Fair Odds\",\"Sharp Bookie\",\"Stake\",\"Event Date\"";
    }
}
