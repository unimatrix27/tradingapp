using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IBrokerTimeIntervalRepository : IRepository<BrokerTimeInterval>
    {
        BrokerTimeInterval GetPreferedDataProvider(string s);
    }
}
