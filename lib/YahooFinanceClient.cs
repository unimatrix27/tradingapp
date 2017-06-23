using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace MyYahooFinance.NET
{
	public class YahooFinanceClient
	{
		private const string BaseUrl = "https://query1.finance.yahoo.com/";
	    private const string BasePath = "/v7/finance/download/";

        private const string RealTimeUrl = "http://finance.yahoo.com/d/quotes.csv?s=";
	    private const string RealTimeSuffix = "&f=abl1pohgt1sv"; // removed n for name

        private enum HistoryType
		{
			DividendHistory = 1,
			Day,
			Week,
			Month,
		}

		public string GetYahooStockCode(string exchange, string code)
		{
			var exchangeHelper = new YahooExchangeHelper();
			return exchangeHelper.GetYahooStockCode(exchange, code);
		}

		public List<YahooHistoricalPriceData> GetDailyHistoricalPriceData(string yahooStockCode, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
		{
			return GetHistoricalPriceData(yahooStockCode, HistoryType.Day, startDate, endDate);
		}

		public List<YahooHistoricalPriceData> GetWeeklyHistoricalPriceData(string yahooStockCode, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
		{
			return GetHistoricalPriceData(yahooStockCode, HistoryType.Week, startDate, endDate);
		}

		public List<YahooHistoricalPriceData> GetMonthlyHistoricalPriceData(string yahooStockCode, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
		{
			return GetHistoricalPriceData(yahooStockCode, HistoryType.Month, startDate, endDate);
		}

		public List<YahooHistoricalDividendData> GetHistoricalDividendData(string yahooStockCode, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
		{
			var dividendHistoryCsv = GetHistoricalDataAsCsv(yahooStockCode, HistoryType.DividendHistory, startDate, endDate);

			var historicalDevidendData = new List<YahooHistoricalDividendData>();
			foreach (var line in dividendHistoryCsv.Split('\n').Skip(1))
			{
				if (string.IsNullOrEmpty(line))
				{
					continue;
				}

				var values = line.Split(',');

				var newDividendData = new YahooHistoricalDividendData
				{
					Date = DateTime.Parse(values[0], CultureInfo.InvariantCulture),
					Dividend = decimal.Parse(values[1], CultureInfo.InvariantCulture),
				};
				historicalDevidendData.Add(newDividendData);
			}

			return historicalDevidendData;
		}

		private List<YahooHistoricalPriceData> GetHistoricalPriceData(string yahooStockCode, HistoryType historyType, DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			var historicalDataCsv = GetHistoricalDataAsCsv(yahooStockCode, historyType, startDate, endDate);

			var historicalPriceData = new List<YahooHistoricalPriceData>();
			foreach (var line in historicalDataCsv.Split('\n').Skip(1))
			{
				if (string.IsNullOrEmpty(line))
				{
					continue;
				}

				var values = line.Split(',');

				var newPriceData = new YahooHistoricalPriceData
				{
					Date = DateTime.Parse(values[0], CultureInfo.InvariantCulture),
					Open = decimal.Parse(values[1], CultureInfo.InvariantCulture),
					High = decimal.Parse(values[2], CultureInfo.InvariantCulture),
					Low = decimal.Parse(values[3], CultureInfo.InvariantCulture),
					Close = decimal.Parse(values[4], CultureInfo.InvariantCulture),
				    AdjClose = decimal.Parse(values[5], CultureInfo.InvariantCulture),
                    Volume = long.Parse(values[6], CultureInfo.InvariantCulture)
				};
				historicalPriceData.Add(newPriceData);
			}

			return historicalPriceData;
		}

		private string GetHistoricalDataAsCsv(string yahooStockCode, HistoryType historyType, DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			var dateRangeOption = string.Empty;
			var addDateRangeOption = startDate.HasValue && endDate.HasValue;
			if (addDateRangeOption)
			{
				var startDateValue = startDate.Value;
				var endDateValue = endDate.Value;

				dateRangeOption = GetDateRangeOption(startDateValue, endDateValue);
			}

			var historyTypeOption = GetHistoryType(historyType);
			var options = $"?{dateRangeOption}{historyTypeOption}";

			var historicalDataCsv = YahooApiRequest(yahooStockCode, options);

			return historicalDataCsv;
		}

	    public YahooRealTimeData GetRealTimeData(string yahooStockCode)
	    {
	        var RealTimeDataCsv = GetRealTimeDataAsCsv(yahooStockCode);


            var values = RealTimeDataCsv.Split(',');

            try
            {
                var realTimeData = new YahooRealTimeData
                {
                    Last = decimal.Parse(values[2].Replace("\"", ""), CultureInfo.InvariantCulture),
                    Ask = values[0] == "N/A" ? decimal.Parse(values[2].Replace("\"", ""), CultureInfo.InvariantCulture) : decimal.Parse(values[0].Replace("\"", ""), CultureInfo.InvariantCulture),
                    Bid = values[1] == "N/A" ? decimal.Parse(values[2].Replace("\"", ""), CultureInfo.InvariantCulture) : decimal.Parse(values[1].Replace("\"", ""), CultureInfo.InvariantCulture),
                    PreviousClose = decimal.Parse(values[3].Replace("\"", ""), CultureInfo.InvariantCulture),
                    Open = decimal.Parse(values[4].Replace("\"", ""), CultureInfo.InvariantCulture),
                    High = decimal.Parse(values[5].Replace("\"", ""), CultureInfo.InvariantCulture),
                    Low = decimal.Parse(values[6].Replace("\"", ""), CultureInfo.InvariantCulture),
                    LastTradeTime = DateTime.Parse(values[7].Replace("\"", ""), CultureInfo.InvariantCulture),
                    //Name = values[8].Replace("\"", ""), 
                    Symbol = values[8].Replace("\"", ""),
                    Volume = long.Parse(values[9].Replace("\"", ""), CultureInfo.InvariantCulture)
                };

                return realTimeData;
            }
            catch { 
                return null;
            }
        }

        private string GetRealTimeDataAsCsv(string yahooStockCode)
	    {

	        var realTimeDataCsv = YahooRealTimeApiRequest(yahooStockCode);

	        return realTimeDataCsv;
	    }

        private string YahooApiRequest(string yahooStockCode, string options)
        {
            var baseAddress = new Uri(BaseUrl);
            var requestUrl = $"{BasePath}{yahooStockCode}{options}&crumb=sV4uFu6Cw4D";
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, new Cookie("B", "ftp66vtci2hub&b=3&s=3c"));
            using (var handler = new HttpClientHandler() { UseCookies = false })
            {
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var message = new HttpRequestMessage(HttpMethod.Get, BaseUrl+requestUrl);
                    message.Headers.Add("Cookie", "B=ftp66vtci2hub&b=3&s=3c;");
                    using (var response = client.SendAsync(message).Result)
                    {
                        var historicalData = response.Content.ReadAsStringAsync().Result;

                        if (response.IsSuccessStatusCode)
                        {
                            return historicalData;
                        }

                        return string.Empty;
                    }
                }
            }
        }
	    private string YahooRealTimeApiRequest(string yahooStockCode)
	    {
	        var requestUrl = $"{RealTimeUrl}{yahooStockCode}{RealTimeSuffix}";

	        using (var client = new HttpClient())
	        {
	            using (var response = client.GetAsync(requestUrl).Result)
	            {
	                var realTimeData = response.Content.ReadAsStringAsync().Result;

	                if (response.IsSuccessStatusCode)
	                {
	                    return realTimeData;
	                }

	                return string.Empty;
	            }
	        }
	    }

        private string GetHistoryType(HistoryType type)
		{
			var optionCode = string.Empty;
			switch (type)
			{
				case HistoryType.DividendHistory:
					optionCode = "1d&events=dividends";
					break;
				case HistoryType.Day:
					optionCode = "1d&events=history";
					break;
				case HistoryType.Week:
					optionCode = "1wk&events=history";
					break;
				case HistoryType.Month:
					optionCode = "1mo&events=history";
					break;
			}

			var option = $"&interval={optionCode}";
			return option;
		}

		private string GetDateRangeOption(DateTimeOffset startDate, DateTimeOffset endDate)
		{
			//var start = $"{GetStartDate(startDate)}";
			//var end = $"{GetEndDate(endDate)}";
		    var start = startDate.ToUnixTimeSeconds();
		    var end = endDate.ToUnixTimeSeconds();

			var option = $"period1={start}&period2={end}";
			return option;
		}

		private string GetStartDate(DateTimeOffset date)
		{
			// API uses zero-based month numbering
			var month = $"&a={date.Month - 1}";
			var day = $"&b={date.Day}";
			var year = $"&c={date.Year}";

			var option = $"{month}{day}{year}";
			return option;
		}

		private string GetEndDate(DateTimeOffset date)
		{
			// API uses zero-based month numbering
			var month = $"&d={date.Month - 1}";
			var day = $"&e={date.Day}";
			var year = $"&f={date.Year}";

			var option = $"{month}{day}{year}";
			return option;
		}
	}
}
