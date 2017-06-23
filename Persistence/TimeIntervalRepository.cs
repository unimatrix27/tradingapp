using System.Linq;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class TimeIntervalRepository : Repository<TimeInterval>, ITimeIntervalRepository
    {
        public TimeIntervalRepository(TradingDbContext context) : base(context)
        {

        }
        public TimeInterval getTimeInterval(string name)
        {
            return context.TimeIntervals.FirstOrDefault(t => t.Name == name);
        }
    }
}
