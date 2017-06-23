using System.Collections.Generic;
using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IBrokerInstrumentRepository : IRepository<BrokerInstrument>
    {
        IEnumerable<BrokerInstrument> GetAllWithNames();
        IEnumerable<BrokerInstrument> GetAllForBroker(string brokerName);
    }

    public interface ITradeRepository : IRepository<Trade>
    {
        IEnumerable<Trade> GetAllFull();
    }

    public interface ITradeStepRepository : IRepository<TradeStep>
    {
        
    }

    public interface IOrderRepository : IRepository<Order>
    {
        
    }
}
