using Microsoft.EntityFrameworkCore;
using trading.Models;

namespace trading.Persistence
{
    public interface ITransientTradingDbContext
    {
        DbSet<BrokerInstrument> BrokerInstruments { get; set; }
        DbSet<BrokerInstrumentScreenerType> BrokerInstrumentScreenerTypes { get; set; }
        DbSet<Broker> Brokers { get; set; }
        DbSet<BrokerSymbol> BrokerSymbols { get; set; }
        DbSet<BrokerTimeInterval> BrokerTimeIntervals { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<Exchange> Exchanges { get; set; }
        DbSet<IndicatorEntry> IndicatorEntries { get; set; }
        DbSet<InstrumentName> InstrumentNames { get; set; }
        DbSet<InstrumentType> InstrumentTypes { get; set; }
        DbSet<PriceEntry> PriceEntries { get; set; }
        DbSet<ScreenerEntry> ScreenerEntries { get; set; }
        DbSet<ScreenerEntryMapping> ScreenerEntryMappings { get; set; }
        DbSet<ScreenerEntryType> ScreenerEntryTypes { get; set; }
        DbSet<ScreenerLine> ScreenerLines { get; set; }
        DbSet<ScreenerReferenceImage> ScreenerReferenceImages { get; set; }
        DbSet<Screener> Screeners { get; set; }
        DbSet<ScreenerType> ScreenerTypes { get; set; }
        DbSet<TimeInterval> TimeIntervals { get; set; }
        DbSet<Signal> Signals { get; set; }
        DbSet<SignalStep> SignalSteps { get; set; }
        DbSet<StopLossRule> StopLossRules { get; set; }
        DbSet<ExitRule> ExitRules { get; set; }
    }
}