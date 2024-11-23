using BettingBot.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot
{
    class IOManager
    {
        //private static string accListFile = "accList.bin";
        private static string betDataFile = "betData.bin";


        public static void saveBetData(List<HorseItem> infoList)
        {
            string path = Directory.GetCurrentDirectory() + "\\" + betDataFile;
            //serialize
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, infoList);
            }
        }

        public static void saveBetData(List<JsonTrade> infoList)
        {
            string path = Directory.GetCurrentDirectory() + "\\" + betDataFile;
            //serialize
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, infoList);
            }
        }

        public static List<HorseItem> readBetData()
        {
            List<HorseItem> ret = new List<HorseItem>();
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\" + betDataFile;
                //deserialize
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    ret = (List<HorseItem>)bformatter.Deserialize(stream);
                    DateTime today = DateTime.Now;
                }
            }
            catch (Exception)
            {
            }
            return ret;
        }

        public static List<JsonTrade> readTradeBetData()
        {
            List<JsonTrade> ret = new List<JsonTrade>();
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\" + betDataFile;
                //deserialize
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    ret = (List<JsonTrade>)bformatter.Deserialize(stream);
                    DateTime today = DateTime.Now;
                }
            }
            catch (Exception)
            {
            }
            return ret;
        }

        public static void removeBetData()
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\" + betDataFile;
                File.Delete(path);
            }
            catch (Exception)
            {

            }
        }
  
    }
}
