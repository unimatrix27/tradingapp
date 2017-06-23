using System;

namespace MyYahooFinance.NET
{
	[Serializable]
	public class YahooHistoricalDividendData
	{
		public DateTime Date { get; set; }
		public decimal Dividend { get; set; }
	}
}
