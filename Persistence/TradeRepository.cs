using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;
using System.Linq;

namespace Trading.Persistence
{
    public class TradeRepository : Repository<Trade>, ITradeRepository{
        public TradeRepository(TradingDbContext context) : base(context) {}

        public IEnumerable<Trade> GetAllFull()
        {
            return this.context.Trades
                .Include(x => x.TradeSteps)
                .Include(x => x.Orders)
                .Include(x => x.Signal)
                    .ThenInclude(x => x.SignalSteps)
                        .ThenInclude(x => x.PriceEntry)
                .Include(x => x.Signal)
                    .ThenInclude(x => x.BrokerInstrument)
                        .ThenInclude(x => x.PriceEntries)
                .Include(x => x.Signal)
                    .ThenInclude(x => x.BrokerInstrument)
                        .ThenInclude(x => x.BrokerSymbol)
                            .ThenInclude(x => x.InstrumentName)
                .Include(x => x.Signal)
                    .ThenInclude(x => x.BrokerInstrument)
                        .ThenInclude(x => x.BrokerSymbol)
                            .ThenInclude(x => x.Exchange)
                                .ThenInclude(x => x.Broker)

                    .Include(x => x.Signal)
                    .ThenInclude(x => x.BrokerInstrument)
                    .ThenInclude(x => x.BrokerSymbol)
                    .ThenInclude(x => x.Exchange)
                    .ThenInclude(x => x.Currency)

                    .Include(x => x.Signal)
                    .ThenInclude(x => x.BrokerInstrument)
                    .ThenInclude(x => x.InstrumentType)


               .Include(x => x.Signal)
                    .ThenInclude(x => x.BrokerInstrument)
                        .ThenInclude(x => x.BrokerInstrumentScreenerTypes)
                            .ThenInclude(x => x.ScreenerType);
                            
        }
    }
}
