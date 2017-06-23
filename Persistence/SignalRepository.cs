using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class SignalRepository : Repository<Signal>, ISignalRepository
    {
        public SignalRepository(TradingDbContext context) : base(context) { }

        public IEnumerable<Signal> GetOpen()
        {
            var trades = this.context.Trades.Include(t => t.TradeSteps)
                .Where(x => x.TradeSteps.OrderByDescending(s => s.Created).FirstOrDefault().Type != TradeStepType.Cancel)
                .Where(x => x.TradeSteps.OrderByDescending(s => s.Created).FirstOrDefault().Type != TradeStepType.Closed)
                .Where(x => x.TradeSteps.OrderByDescending(s => s.Created).FirstOrDefault().Type != TradeStepType.Hide);
            var result = context.Signals
                .Include(x => x.SignalSteps)
                .ThenInclude(y => y.PriceEntry)
                .Include(x => x.Trades)
                .Include(x => x.BrokerInstrument)
                .ThenInclude(x => x.BrokerInstrumentScreenerTypes)
                .ThenInclude(x => x.ScreenerType)
                .Include(x => x.BrokerInstrument)
                .ThenInclude(x => x.BrokerSymbol)
                .ThenInclude(x => x.InstrumentName)

                .Where(x => x.SignalSteps.All(y => y.SignalStepType !=
                                                   SignalStepType.Cancel))
                .Where(x => trades
                    .All(t => t.Signal.BrokerInstrumentId != x.BrokerInstrumentId));
                
               //.OrderBy(x => x.BrokerInstrument.BrokerInstrumentScreenerTypes.First().ScreenerType.Name);
             //   .ThenBy(x => x.BrokerInstrument.BrokerSymbol.InstrumentName.Name)
	            //.ThenBy(x => x.Id)
	            //.ThenBy(x => x.SignalSteps.OrderByDescending(y => y.PriceEntry.TimeStamp).First().PriceEntry.TimeStamp);

            return result;
	            
        }
    }
}
