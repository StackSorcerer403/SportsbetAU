using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BettingBot
{
    public class Setting
    {
        private static Setting _instance = null;

        public static Setting instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Setting();

                return _instance;
            }
        }
        public string EXData { get; set; }
        public string proxy { get; set; }
        public string proxyUser { get; set; }
        public string proxyPass { get; set; }
        public string serverURL { get; set; }
        public string betUser { get; set; }
        public string betPassword { get; set; }
        public string birthDay { get; set; }
        public string fingerPrint { get; set; }
        public string apiKey { get; set; }
        public string currency { get; set; }
        public double minOdds { get; set; }
        public double maxOdds { get; set; }
        public double flatStake { get; set; }

        public double beforeKickoff { get; set; }

        public double totalReturn { get; set; }

        public double maxValue { get; set; }
        public double minValue { get; set; }
        public double maxPercent { get; set; }
        public double minPercent { get; set; }
        public dynamic BotSetting { get; set; }
        public bool isOnline { get; set; }
        public bool enableHorse { get; internal set; }
        public bool enableTrade { get; internal set; }
        public bool enableHarness { get; internal set; }
        public bool enableDog { get; internal set; }

        public string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36";
        public Setting()
        {
        }

        public string ReadRegistry(string KeyName)
        {
            try
            {
                return Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("Noah_sportsbet").GetValue(KeyName, (object)"").ToString();
            }
            catch
            {

            }
            return string.Empty;
        }

        public void WriteRegistry(string KeyName, string KeyValue)
        {
            try
            {
                Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("Noah_sportsbet").SetValue(KeyName, (object)KeyValue);
            }
            catch
            {

            }
        }

        public void saveSettingInfo()
        {
            WriteRegistry("proxy", Setting.instance.proxy);
            WriteRegistry("proxyUser", Setting.instance.proxyUser);
            WriteRegistry("proxyPass", Setting.instance.proxyPass);
            WriteRegistry("serverURL", Setting.instance.serverURL);
            WriteRegistry("birthDay", Setting.instance.birthDay);
            WriteRegistry("betUser", Setting.instance.betUser);
            WriteRegistry("betPassword", Setting.instance.betPassword);
            WriteRegistry("fingerPrint", Setting.instance.fingerPrint);
            WriteRegistry("apiKey", Setting.instance.apiKey);
            WriteRegistry("currency", Setting.instance.currency);

            WriteRegistry("beforeKickoff", Setting.instance.beforeKickoff.ToString());
            WriteRegistry("totalReturn", Setting.instance.totalReturn.ToString());
            WriteRegistry("minOdds"    , Setting.instance.minOdds.ToString());
            WriteRegistry("maxOdds"    , Setting.instance.maxOdds.ToString());

            WriteRegistry("minPercent", Setting.instance.minPercent.ToString());
            WriteRegistry("maxPercent", Setting.instance.maxPercent.ToString());

            WriteRegistry("minValue", Setting.instance.minValue.ToString());
            WriteRegistry("maxValue", Setting.instance.maxValue.ToString());

            WriteRegistry("flatStake", Setting.instance.flatStake.ToString());

            WriteRegistry("enableHorse", Setting.instance.enableHorse?"true":"false");
            WriteRegistry("enableDog", Setting.instance.enableDog? "true" : "false");
            WriteRegistry("enableHarness", Setting.instance.enableHarness? "true" : "false");

            WriteRegistry("enableTrade", Setting.instance.enableTrade ? "true" : "false");
        }

        public void loadSettingInfo()
        {
            Setting.instance.proxy = ReadRegistry("proxy");
            Setting.instance.proxyUser = ReadRegistry("proxyUser");
            Setting.instance.proxyPass = ReadRegistry("proxyPass");

            Setting.instance.serverURL   = ReadRegistry("serverURL");

            Setting.instance.birthDay = ReadRegistry("birthDay");
            Setting.instance.betUser = ReadRegistry("betUser");
            Setting.instance.betPassword = ReadRegistry("betPassword");
            Setting.instance.fingerPrint = ReadRegistry("fingerPrint");

            Setting.instance.apiKey = ReadRegistry("apiKey");
            Setting.instance.currency = ReadRegistry("currency");

            Setting.instance.enableHorse = ReadRegistry("enableHorse") == "true";
            Setting.instance.enableDog = ReadRegistry("enableDog") == "true";
            Setting.instance.enableHarness = ReadRegistry("enableHarness") == "true";

            Setting.instance.enableTrade = ReadRegistry("enableTrade") == "true";

            

            Setting.instance.beforeKickoff = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("beforeKickoff")) ? "300" : ReadRegistry("beforeKickoff"));
            Setting.instance.totalReturn = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("totalReturn")) ? "8000" : ReadRegistry("totalReturn"));

            Setting.instance.minOdds       = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("minOdds")) ? "1.2" : ReadRegistry("minOdds"));
            Setting.instance.maxOdds       = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("maxOdds")) ? "6" : ReadRegistry("maxOdds"));

            Setting.instance.flatStake = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("flatStake")) ? "2" : ReadRegistry("flatStake"));
            Setting.instance.minValue = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("minValue")) ? "10" : ReadRegistry("minValue"));
            Setting.instance.maxValue = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("maxValue")) ? "100" : ReadRegistry("maxValue"));

            Setting.instance.minPercent = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("minPercent")) ? "10" : ReadRegistry("minPercent"));
            Setting.instance.maxPercent = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("maxPercent")) ? "100" : ReadRegistry("maxPercent"));

        }
    }
}
