using System.Linq;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class BrokerTimeIntervalRepository : Repository<BrokerTimeInterval>, IBrokerTimeIntervalRepository
    {
        public BrokerTimeIntervalRepository(TradingDbContext context) : base(context)
        {

        }
        public BrokerTimeInterval GetPreferedDataProvider(string tiShortName)
        {
            return context.BrokerTimeIntervals.Where(t => t.TimeInterval.Name == tiShortName).OrderByDescending(t => t.dataPriority).First();
        }
    }
}
