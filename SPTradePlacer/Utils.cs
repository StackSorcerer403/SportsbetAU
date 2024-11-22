using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BettingBot
{
    public class Utils
    {
        private static NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint;
        private static CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");
        static Random random = new Random();

        public static double ParseToDouble(string str)
        {
            culture = CultureInfo.CreateSpecificCulture(Setting.instance.currency);
            double value = 0;
            double.TryParse(str, style, culture, out value);
            return value;
        }

        public static string AddZeroLetter(int value)
        {
            if (value < 10) return string.Format("0{0}", value);
            else return value.ToString();
        }

        public static string getCSRF()
        {
            return getTick().ToString("X2");
        }

        public static long getTick()
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            long timestamp = (long)t.TotalMilliseconds;
            return timestamp;
        }

        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("x2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("x");
        }

        public static bool TryParseGuid(string guidString, out Guid guid)
        {
            if (guidString == null) throw new ArgumentNullException("guidString");
            try
            {
                guid = new Guid(guidString);
                return true;
            }
            catch (FormatException)
            {
                guid = default(Guid);
                return false;
            }
        }

        public static double FractionToDouble(string fraction)
        {
            double result;

            if (double.TryParse(fraction, out result))
            {
                return result;
            }

            string[] split = fraction.Split(new char[] { ' ', '/' });

            if (split.Length == 2 || split.Length == 3)
            {
                int a, b;

                if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
                {
                    if (split.Length == 2)
                    {
                        return 1 + Math.Floor((double)100 * a / b) / 100;
                    }

                    int c;

                    if (int.TryParse(split[2], out c))
                    {
                        return a + (double)b / c;
                    }
                }
            }

            throw new FormatException("Not a valid fraction. => " + fraction);
        }

        public static double calculateValuePercent(double softOdds, double baseOdds1, double baseOdds2, ref string logText)
        {
            double impliedProb1 = 1 / baseOdds1;
            double impliedProb2 = 1 / baseOdds2;
            double margin = impliedProb1 + impliedProb2 - 1;
            double trueProb1 = impliedProb1 / (1 + margin);
            double trueProb2 = impliedProb2 / (1 + margin);
            double fairOdds1 = 1 / trueProb1;
            double fairOdds2 = 1 / trueProb2;
            double valuePercent = 0;
            valuePercent = (softOdds - fairOdds1) / fairOdds1 * 100;
            valuePercent = Math.Floor(valuePercent * 100) / 100;
            logText = string.Format("BaseOdds: {0}, Margin: {1}, FairOdds: {2}, Value: {3}",
                baseOdds1.ToString("N2"),
                (margin * 100).ToString("N2"),
                fairOdds1.ToString("N2"),
                valuePercent.ToString("N2"));
            // Filter if margin is negative.
            if (margin < 0) return -100;
            return valuePercent;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            if (LastString != null)
            {
                STR = STR.Substring(Pos1);
                int Pos2 = STR.IndexOf(LastString);
                if (Pos2 > 0)
                    FinalString = STR.Substring(0, Pos2);
                else
                    FinalString = STR;
            }
            else
            {
                FinalString = STR.Substring(Pos1);
            }

            return FinalString;
        }

        internal static int GetRandValue(int v1, int v2, bool v3)
        {
            Random rand = new Random();
            return rand.Next(v1, v2);
        }
    }
}
