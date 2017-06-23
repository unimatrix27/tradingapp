using System;
using System.Security.Policy;

namespace MyYahooFinance.NET
{
	[Serializable]
	public class YahooRealTimeData
	{

        public decimal Ask { get; set; }
        public decimal Bid { get; set; }
	    public decimal Last { get; set; }
	    public decimal PreviousClose { get; set; }
	    public decimal Open { get; set; }
	    public decimal High { get; set; }
		public decimal Low { get; set; }
        public DateTime LastTradeTime { get; set; }
		public long Volume { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }

	}
}
