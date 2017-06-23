using System;
using System.Collections.Generic;

namespace MyYahooFinance.NET
{
	internal class YahooExchangeHelper
	{
		public string GetYahooStockCode(string exchange, string code)
		{
			var exchangeSuffix = GetYahooExchangeSuffix(exchange);

			var yahooStockCode = !string.IsNullOrEmpty(exchangeSuffix) ? $"{code}.{exchangeSuffix}" : code;

			return yahooStockCode.ToUpperInvariant();
		}

		private string GetYahooExchangeSuffix(string exchange)
		{
			string suffix;
			if (_exchanges.TryGetValue(exchange.ToUpperInvariant(), out suffix))
			{
				return suffix;
			}

			throw new Exception($"The \"{exchange.ToUpperInvariant()}\" exchange is not supported.");
		}

		private readonly Dictionary<string, string> _exchanges = new Dictionary<string, string>()
		{
			//Asia Pacific Stock Exchanges
			{"ASX", "AX"},
			{"HKG", "HK"},
			{"SHA", "SS"},
			{"SHE", "SZ"},
			{"NSE", "NS"},
			{"BSE", "BO"},
			{"JAK", "JK"},
			{"SEO", "KS"},
			{"KDQ", "KQ"},
			{"KUL", "KL"},
			{"NZE", "NZ"},
			{"SIN", "SI"},
			{"TPE", "TW"},
			//European Stock Exchanges
			{"WBAG", "VI"},
			{"EBR", "BR"},
			{"EPA", "PA"},
			{"BER", "BE"},
			{"ETR", "DE"},
			{"FRA", "F"},
			{"STU", "SG"},
			{"ISE", "IR"},
			{"BIT", "MI"},
			{"AMS", "AS"},
			{"OSL", "OL"},
			{"ELI", "LS"},
			{"MCE", "MA"},
			{"VTX", "VX"},
			{"LON", "L"},
			//Middle Eastern Stock Exchanges
			{"TLV", "TA"},
			//North American Stock Exchanges
			{"TSE", "TO"},
			{"CVE", "V"},
			{"AMEX", "AMEX"},
			{"NASDAQ", "NASDAQ"},
			{"NYSE", "NYSE"},
		};
	}
}
