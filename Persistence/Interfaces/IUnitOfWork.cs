using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trading.Models;
using Trading.Persistence.Transfer;

namespace Trading.Persistence.Interfaces
{

    public interface IUnitOfWork : IDisposable
    {
        ITimeIntervalRepository TimeIntervals { get; }
        IBrokerTimeIntervalRepository BrokerTimeIntervals { get; }
        IBrokerRepository Brokers { get; }
        IInstrumentNameRepository InstrumentNames { get; }
        IBrokerInstrumentRepository BrokerInstruments { get; }
        IPriceEntryRepository PriceEntries { get; }
        IScreenerEntryMappingRepository ScreenerEntryMappings { get; }
        IScreenerEntryTypeRepository ScreenerEntryTypes { get; }
        IBrokerInstrumentScreenerTypeRepository  BrokerInstrumentScreenerTypes { get; }
        IScreenerTypeRepository ScreenerTypes { get; }
        
        IBrokerSymbolRepository BrokerSymbols { get; }
        ICurrencyRepository Currencies { get; }
        IExchangeRepository Exchanges { get; }
        IInstrumentTypeRepository InstrumentTypes { get; }
        
        IScreenerRepository Screeners { get; }
        IScreenerLineRepository ScreenerLines { get; }
        IScreenerReferenceImageRepository ScreenerReferenceImages { get; }
        IScreenerEntryRepository ScreenerEntries { get; }
        IIndicatorEntryRepository IndicatorEntries { get; }
        ITradeRepository Trades { get; }
        ITradeStepRepository TradeSteps { get; }
        IOrderRepository Orders { get; }
        ISignalRepository Signals {get;}

        int? Complete();

        IEnumerable<PriceEntryWithIndications> GetAllWithIndicatorAndScreener(string screenerTypeName = null, int brokerInstrumentId = 0, DateTimeOffset? timeStamp = null, int timeIntervalId = 1, int records = 1);
        IEnumerable<object> GetAllWithIndicatorAndScreenerFlat(string screenerTypeName = null, int brokerInstrumentId = 0, DateTimeOffset? timeStamp = null, int timeIntervalId = 1, int records = 1);

        IEnumerable<object> GetAllWithIndicatorAndScreenerFlat(string screener, int brokerInstrumentId, int back,
            int count, string timeInterval = "D1");

        void ProcessSignals();

        Trade CreateTrade(int signalId);
        Trade CancelTrade(int tradeId);
        Trade UndoLastTradeStep(int tradeId);
        Trade ConfirmPlaceTrade(int tradeId, int? size=null, decimal? stop=null, decimal? entry=null, decimal? tp=null);
        Trade ConfirmFilled(int tradeId, decimal executedLevel,int size=0);

        Trade ConfirmExit(int tradeId, decimal level);
        Trade HideTrade(int tradeId);
        Signal DiscardSignal(int signalId);
        
        Trade UpdateTradeFromSignal(int tradeId);

    }
}
