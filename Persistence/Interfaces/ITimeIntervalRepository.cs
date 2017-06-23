using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface ITimeIntervalRepository : IRepository<TimeInterval>
    {
        TimeInterval getTimeInterval(string name);
    }
}
