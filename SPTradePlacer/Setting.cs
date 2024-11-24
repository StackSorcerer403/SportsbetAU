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
        public double flatStake { get; set; }
        public double beforeKickoff { get; set; }
        public double totalReturn { get; set; }
        public int slipCount { get; set; }
        public dynamic BotSetting { get; set; }
        public bool isOnline { get; set; }        
        public bool enableAutoSlip { get; internal set; }
        public bool enableAutoStaker { get; internal set; }

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
            WriteRegistry("slipCount", Setting.instance.slipCount.ToString());
            WriteRegistry("flatStake", Setting.instance.flatStake.ToString());
            WriteRegistry("enableAutoSlip", Setting.instance.enableAutoSlip ? "true" : "false");
            WriteRegistry("enableAutoStaker", Setting.instance.enableAutoStaker ? "true" : "false");

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

            Setting.instance.enableAutoSlip = ReadRegistry("enableAutoSlip") == "true";
            Setting.instance.enableAutoStaker = ReadRegistry("enableAutoStaker") == "true";
            Setting.instance.beforeKickoff = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("beforeKickoff")) ? "300" : ReadRegistry("beforeKickoff"));
            Setting.instance.totalReturn = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("totalReturn")) ? "8000" : ReadRegistry("totalReturn"));
            Setting.instance.slipCount= Convert.ToInt16(string.IsNullOrEmpty(ReadRegistry("slipCount")) ? "10" : ReadRegistry("slipCount"));

            Setting.instance.flatStake = Utils.ParseToDouble(string.IsNullOrEmpty(ReadRegistry("flatStake")) ? "2" : ReadRegistry("flatStake"));
        }
    }
}
