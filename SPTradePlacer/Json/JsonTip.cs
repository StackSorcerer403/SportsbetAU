using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot.Json
{
	[Serializable]
	public class JsonTip
	{
		public string id { get; set; }
		public string dbId { get; set; }
		public string Tipster { get; set; }
		public string Sport { get; set; }
		public string League { get; set; }
		public string Match { get; set; }
		public string Home { get; set; }
		public string Away { get; set; }
		public double Odds { get; set; }
		public decimal Line { get; set; }
		public string Pick { get; set; }
		public string Market { get; internal set; }
		public string Period { get; internal set; }
		public double Percent { get; internal set; }
		public double stake { get; internal set; }
		public string RawData { get; internal set; }
		public JsonTip()
		{
		}
	}
}
