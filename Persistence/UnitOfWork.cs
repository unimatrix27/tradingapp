using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FluentDateTime;
using FluentDateTimeOffset;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;
using Trading.Persistence.Transfer;



namespace Trading.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ITimeIntervalRepository timeIntervals, IBrokerTimeIntervalRepository brokerTimeIntervals, IBrokerRepository brokers, IInstrumentNameRepository instrumentNames, IBrokerInstrumentRepository brokerInstruments, IPriceEntryRepository priceEntries, IScreenerEntryMappingRepository screenerEntryMappings, IScreenerEntryTypeRepository screenerEntryTypes, IBrokerInstrumentScreenerTypeRepository brokerInstrumentScreenerTypes, IScreenerTypeRepository screenerTypes, IBrokerSymbolRepository brokerSymbols, ICurrencyRepository currencies, IExchangeRepository exchanges, IInstrumentTypeRepository instrumentTypes, IScreenerRepository screeners, IScreenerLineRepository screenerLines, IScreenerReferenceImageRepository screenerReferenceImages, IScreenerEntryRepository screenerEntries, IIndicatorEntryRepository indicatorEntries, ITradeRepository trades, ITradeStepRepository tradeSteps, IOrderRepository orders, ISignalRepository signals, ISignalStepRepository signalSteps, IStopLossRuleRepository stopLossRules, IExitRuleRepository exitRules)
        {
            this.TimeIntervals = timeIntervals;
            this.BrokerTimeIntervals = brokerTimeIntervals;
            this.Brokers = brokers;
            this.InstrumentNames = instrumentNames;
            this.BrokerInstruments = brokerInstruments;
            this.PriceEntries = priceEntries;
            this.ScreenerEntryMappings = screenerEntryMappings;
            this.ScreenerEntryTypes = screenerEntryTypes;
            this.BrokerInstrumentScreenerTypes = brokerInstrumentScreenerTypes;
            this.ScreenerTypes = screenerTypes;
            this.BrokerSymbols = brokerSymbols;
            this.Currencies = currencies;
            this.Exchanges = exchanges;
            this.InstrumentTypes = instrumentTypes;
            this.Screeners = screeners;
            this.ScreenerLines = screenerLines;
            this.ScreenerReferenceImages = screenerReferenceImages;
            this.ScreenerEntries = screenerEntries;
            this.IndicatorEntries = indicatorEntries;
            this.Trades = trades;
            this.TradeSteps = tradeSteps;
            this.Orders = orders;
            this.Signals = signals;
            this.SignalSteps = signalSteps;
            this.StopLossRules = stopLossRules;
            this.ExitRules = exitRules;

        }
        public ITimeIntervalRepository TimeIntervals { get; }
        public IBrokerTimeIntervalRepository BrokerTimeIntervals { get; }
        public IBrokerRepository Brokers { get; }
        public IInstrumentNameRepository InstrumentNames { get; }
        public IBrokerInstrumentRepository BrokerInstruments { get; }
        public IPriceEntryRepository PriceEntries { get; }
        public IScreenerEntryMappingRepository ScreenerEntryMappings { get; }
        public IScreenerEntryTypeRepository ScreenerEntryTypes { get; }
        public IBrokerInstrumentScreenerTypeRepository BrokerInstrumentScreenerTypes { get; }
        public IScreenerTypeRepository ScreenerTypes { get; }
        public IBrokerSymbolRepository BrokerSymbols { get; }
        public ICurrencyRepository Currencies { get; }
        public IExchangeRepository Exchanges { get; }
        public IInstrumentTypeRepository InstrumentTypes { get; }
        public IScreenerRepository Screeners { get; }
        public IScreenerLineRepository ScreenerLines { get; }
        public IScreenerReferenceImageRepository ScreenerReferenceImages { get; }
        public IScreenerEntryRepository ScreenerEntries { get; }
        public IIndicatorEntryRepository IndicatorEntries { get; }
        public ITradeRepository Trades { get; }
        public ITradeStepRepository TradeSteps { get; }
        public IOrderRepository Orders { get; }
        public ISignalRepository Signals { get; }
        public ISignalStepRepository SignalSteps { get; }
        public IStopLossRuleRepository StopLossRules { get; }
        public IExitRuleRepository ExitRules { get; }

        public int? Complete()
        {

            return context?.SaveChanges();
        }

        public void Dispose()
        {
            context?.Dispose();
        }

        private readonly TradingDbContext context;
        private readonly ILogger<UnitOfWork> logger;

        public UnitOfWork(TradingDbContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            this.logger = logger;
            TimeIntervals = new TimeIntervalRepository(context);
            BrokerTimeIntervals = new BrokerTimeIntervalRepository(context);
            Brokers = new BrokerRepository(context);
            InstrumentNames = new InstrumentNameRepository(context);
            BrokerInstruments = new BrokerInstrumentRepository(context);
            PriceEntries = new PriceEntryRepository(context, this.logger);
            ScreenerEntryMappings = new ScreenerEntryMappingRepository(context);
            ScreenerEntryTypes = new ScreenerEntryTypeRepository(context);
            BrokerInstrumentScreenerTypes = new BrokerInstrumentScreenerTypeRepository(context);
            ScreenerTypes = new ScreenerTypeRepository(context);
            Screeners = new ScreenerRepository(context);
            BrokerSymbols = new BrokerSymbolRepository(context);
            Currencies = new CurrencyRepository(context);
            Exchanges = new ExchangeRepository(context);
            InstrumentTypes = new InstrumentTypeRepository(context);
            ScreenerReferenceImages = new ScreenerReferenceImageRepository(context);
            ScreenerLines = new ScreenerLineRepository(context);
            ScreenerEntries = new ScreenerEntryRepository(context);
            IndicatorEntries = new IndicatorEntryRepository(context);
            Trades = new TradeRepository(context);
            TradeSteps = new TradeStepRepository(context);
            Signals = new SignalRepository(context);
            Orders = new OrderRepository(context);
        }

        private int getR()
        {
            return 300;
        }

        private IEnumerable<PriceEntryWithIndications> getAllWithIndicatorAndScreener(string screenerTypeName = null, int brokerInstrumentId = 0, DateTimeOffset? timeStamp = null, int timeIntervalId = 1, int records = 1)
        {

            var baseSet = context.PriceEntries.OrderByDescending<PriceEntry, DateTimeOffset>(p => p.TimeStamp)
                .Include(x => x.IndicatorEntries)
                //.Include(x => x.BrokerInstrument)
                //.ThenInclude(x => x.ScreenerLines)
                //.ThenInclude(x => x.Screener)
                //.Include(x => x.BrokerInstrument)
                //.ThenInclude(x => x.ScreenerLines)
                //.ThenInclude(x => x.ScreenerEntries)
                //.ThenInclude(x => x.ScreenerEntryType)
                .Include(x => x.BrokerInstrument)
                .ThenInclude(x => x.BrokerInstrumentScreenerTypes)
                .ThenInclude(x => x.ScreenerType)
                //.Where(t => t.TimeIntervalId == 1 && t.BrokerInstrumentId == 11)
                .Where(x => x.IndicatorEntries.Count() != 0)
                .Where(x => x.BrokerInstrument.ScreenerLines.FirstOrDefault(
                                y => y.Screener.TimeStamp.Date == x.TimeStamp.Date) != null)
                .Where(x => x.TimeIntervalId == timeIntervalId);

            if (screenerTypeName != null)
                baseSet = baseSet.Where(x => x.BrokerInstrument.BrokerInstrumentScreenerTypes.Any(y => y.ScreenerType.Name == screenerTypeName));

            if (brokerInstrumentId != 0)
                baseSet = baseSet.Where(x => x.BrokerInstrumentId == brokerInstrumentId);

            if (timeStamp == null) timeStamp = DateTimeOffset.Now;
            IEnumerable<PriceEntry> baseSetList = baseSet.ToList();
            var distinctTimeStamps = baseSetList.Select(x => x.TimeStamp).Distinct();
            //var wurst = distinctTimeStamps.ToList();
            //if (timeIntervalId == 1) distinctTimeStamps = distinctTimeStamps.Select(x =>  x.Date).Distinct();


            distinctTimeStamps = distinctTimeStamps.OrderByDescending(x => x).Where(x => x <= timeStamp);

            var startTimeStamp = distinctTimeStamps.Skip(records).FirstOrDefault();

            baseSetList = baseSetList.Where(x => x.TimeStamp <= timeStamp);
            if (startTimeStamp != null)
                baseSetList = baseSetList.Where(x => x.TimeStamp > startTimeStamp);

            IEnumerable<ScreenerLine> allScreenerLines = context.ScreenerLines.Include(x => x.Screener).Include(x => x.ScreenerEntries).ThenInclude(x => x.ScreenerEntryType).ToList();

            var result =
                baseSetList
                    .Select(x => new PriceEntryWithIndications()

                    {
                        p = x,
                        ind = x.IndicatorEntries,
                        scr = allScreenerLines.Where(t => t.BrokerInstrumentId == x.BrokerInstrumentId)
                            .FirstOrDefault(y => y.Screener.TimeStamp.Date == x.TimeStamp.Date).ScreenerEntries

                    });
            return result;
        }

        public IEnumerable<PriceEntryWithIndications> GetAllWithIndicatorAndScreener(string screenerTypeName = null, int brokerInstrumentId = 0, DateTimeOffset? timeStamp = null, int timeIntervalId = 1, int records = 1)
        {
            return getAllWithIndicatorAndScreener(screenerTypeName, brokerInstrumentId, timeStamp, timeIntervalId, records);
        }

        public IEnumerable<PriceEntryWithIndications> GetAllWithIndicatorAndScreener(string screener = null,
            int brokerInstrumentId = 0, int back = 0, int count = 1, string timeInterval = "D1")
        {
            var ti = context.TimeIntervals.FirstOrDefault(x => x.Name == timeInterval);
            return GetAllWithIndicatorAndScreener(screener, brokerInstrumentId, back, count, ti.Id);
        }

        public IEnumerable<PriceEntryWithIndications> GetAllWithIndicatorAndScreener(string screener = null,
            int brokerInstrumentId = 0, int back = 0, int count = 1, int timeInterval = 1)
        {
            var ti = context.TimeIntervals.FirstOrDefault(x => x.Id == timeInterval);
            var backts = ti == null ? TimeSpan.Zero : TimeSpan.FromTicks(ti.DurationTicks * back);
            int tiid = ti?.Id ?? 1;
            return GetAllWithIndicatorAndScreener(screener, brokerInstrumentId, DateTimeOffset.Now - backts, tiid, count);
        }

        public IEnumerable<object> GetAllWithIndicatorAndScreenerFlat(string screener,
            int brokerInstrumentId, int back, int count, string timeInterval = "D1")
        {
            var ti = context.TimeIntervals.FirstOrDefault(x => x.Name == timeInterval);
            var backts = ti == null ? TimeSpan.Zero : TimeSpan.FromTicks(ti.DurationTicks * back);
            int tiid = ti?.Id ?? 1;
            return GetAllWithIndicatorAndScreenerFlat(screener, brokerInstrumentId, DateTimeOffset.Now - backts, tiid, count);
        }


        public IEnumerable<object> GetAllWithIndicatorAndScreenerFlat(string screenerTypeName = null, int brokerInstrumentId = 0, DateTimeOffset? timeStamp = null, int timeIntervalId = 1, int records = 1)
        {
            return getAllWithIndicatorAndScreener(screenerTypeName, brokerInstrumentId, timeStamp, timeIntervalId, records)
                .Select(x => new
                {
                    sname = x.p.BrokerInstrument.BrokerInstrumentScreenerTypes.FirstOrDefault().ScreenerType.Name,
                    cur = x.p.BrokerInstrument.BrokerSymbol.Exchange.Currency.Name,
                    sym = x.p.BrokerInstrument.BrokerSymbol.Name,
                    iname = x.p.BrokerInstrument.BrokerSymbol.InstrumentName.Name,
                    ts = x.p.TimeStamp.Date,
                    High = x.p.High,
                    Low = x.p.Low,
                    SpanA = x.ind.FirstOrDefault(y => y.Type == IndicatorDataType.Wolke_SpanA).Data,
                    SpanB = x.ind.FirstOrDefault(y => y.Type == IndicatorDataType.Wolke_SpanB).Data,
                    OSpanA = x.ind.FirstOrDefault(y => y.Type == IndicatorDataType.OrigWolke_SpanA).Data,
                    OSpanB = x.ind.FirstOrDefault(y => y.Type == IndicatorDataType.OrigWolke_SpanB).Data,
                    wolke = x.scr.FirstOrDefault(s => s.ScreenerEntryType.Name == "Wolke") == null
                        ? 0
                        : x.scr.FirstOrDefault(s => s.ScreenerEntryType.Name == "Wolke").Bg,
                    ICER = x.scr.FirstOrDefault(s => s.ScreenerEntryType.Name == "ICER") == null
                        ? 0
                        : x.scr.FirstOrDefault(s => s.ScreenerEntryType.Name == "ICER").Bg
                }).ToList();

        }

        public void ProcessSignals()
        {
            // offne signale fuer dieses BI ueber signaltransactions fuer BI


            DateTimeOffset? lastTimeStamp = null;
            var timeIntervals = context.PriceEntries.Select(x => x.TimeInterval).Distinct();
            foreach (var ti in timeIntervals)
            {
                var biSignals = context.Signals
                    .Include(y => y.SignalSteps).ThenInclude(s => s.PriceEntry)
                    .Include(y => y.Trades).ThenInclude(z => z.TradeSteps)
                    .Where(x => x.TimeIntervalId == ti.Id).ToList();
                var openSignals =
                    biSignals.Where(x => x.SignalSteps.All(t => t.SignalStepType != SignalStepType.Cancel)).ToList();



                //var temptt = biSignals.SelectMany(x => x.SignalSteps).ToList();

                // lastTimeStamp= temptt.Max(y => y.PriceEntry.TimeStamp);
                var allPrices = context.PriceEntries.Where(x => x.TimeIntervalId == ti.Id).ToList();
                var count = allPrices.Select(x => x.TimeStamp).Distinct().Count();
                //var allIds = context.BrokerInstruments.Where(x => x.Id == 179).Select(x => x.Id).ToList();
                var allIds = context.BrokerInstruments.Select(x => x.Id).ToList();
                var pfullAll = GetAllWithIndicatorAndScreener(timeInterval: ti.Id, count: count).ToList();
                //var pfullAll = GetAllWithIndicatorAndScreener(timeInterval: ti.Id, count: count,brokerInstrumentId:104).ToList();


                foreach (var brokerInstrumentId in allIds)
                {

                    try
                    {
                        lastTimeStamp = biSignals.Where(x => x.BrokerInstrumentId == brokerInstrumentId)
                            .SelectMany(x => x.SignalSteps).Max(y => y.PriceEntry.TimeStamp);
                    }
                    catch
                    {
                        lastTimeStamp = DateTimeOffset.FromUnixTimeSeconds(0);
                    }

                    var prices = allPrices.Where(x => x.BrokerInstrumentId == brokerInstrumentId)
                        .Where(x => x.TimeStamp > lastTimeStamp).OrderBy(x => x.TimeStamp);

                    var pfull = pfullAll.Where(x => x.p.BrokerInstrumentId == brokerInstrumentId);

                    foreach (var price in prices)
                    {
                        bool pendingComplete = false;
                        //ICER bestehende

                        // ICER ermitteln ob neues signal da ist.

                        var p = pfull.SkipWhile(x => x.p.TimeStamp != price.TimeStamp).Take(2).ToList();
                        if (p.Count != 2) continue;

                        var i0 = p[0].scr.FirstOrDefault(x => x.ScreenerEntryType.Name == "ICER").Bg;
                        var i1 = p[1].scr.FirstOrDefault(x => x.ScreenerEntryType.Name == "ICER").Bg;

                        var w0 = p[0].scr.FirstOrDefault(x => x.ScreenerEntryType.Name == "Wolke").Bg;
                        var w1 = p[1].scr.FirstOrDefault(x => x.ScreenerEntryType.Name == "Wolke").Bg;

                        var s0 = p[0].scr.FirstOrDefault(x => x.ScreenerEntryType.Name == "Setter").Bg;
                        var welle0 = p[0].scr.FirstOrDefault(x => x.ScreenerEntryType.Name == "Welle").Bg;

                        var b10 =  p[0].scr.FirstOrDefault(x => x.ScreenerEntryType.Name == "B10").Bg;

                        var name = context.BrokerInstruments.Where(b => b.Id == brokerInstrumentId)
                            .Include(b => b.BrokerSymbol).ThenInclude(n => n.InstrumentName).FirstOrDefault()
                            .BrokerSymbol.InstrumentName.Name;
                        TradeDirection? newDirection = null;
// ICER ////////////////////////////////////////////////////////

                        newDirection = null;

                        if ((i0 == CellColor.Green && i1 != CellColor.Green && w0 == CellColor.Red)
                            || (i0 == CellColor.Green && w0 == CellColor.Red && w1 != CellColor.Red))
                            newDirection = TradeDirection.Short;
                        if ((i0 == CellColor.Red && i1 != CellColor.Red && w0 == CellColor.Green)
                            || (i0 == CellColor.Red && w0 == CellColor.Green && w1 != CellColor.Green))
                            newDirection = TradeDirection.Long;

                        var openICERs = openSignals.Where(x => x.BrokerInstrumentId == brokerInstrumentId && x.SignalType == SignalType.Icer);

                        if (openICERs.Count() > 1)
                        {

                            throw (new Exception("too many open signals, something in the code is wrong"));
                        }

                        // ist schon ein laufendes signal da??  // ist schon ein laufendes signal da?? 
                        var openIcerSignal = openICERs.FirstOrDefault(x => x.SignalType == SignalType.Icer);
                        if (openIcerSignal != null)
                        {
                            var lastSignalStep = openIcerSignal.SignalSteps.OrderByDescending(x => x.Updated).First();
                            // ist auch ein neues da? dann laufendes abbrechen
                            if (newDirection != null)
                            {
                                var signalStepCancel =
                                    new SignalStep
                                    {
                                        SignalId = openIcerSignal.Id,
                                        SignalStepType = SignalStepType.Cancel,
                                        PriceEntry = price,
                                        Reason = "neues Signal"
                                    };
                                context.SignalSteps.Add(signalStepCancel);
                                AddSignalStep(signalStepCancel);
                                pendingComplete = true;

                            }
                            else // kein neues da
                            {
                                // neues hoch oder tief
                                if ((openIcerSignal.TradeDirection == TradeDirection.Long &&
                                     lastSignalStep.Sl > price.Low) ||
                                    (openIcerSignal.TradeDirection == TradeDirection.Short &&
                                     lastSignalStep.Sl < price.High))
                                {
                                    // icer nicht mehr farbig dann abbrechen
                                    if ((openIcerSignal.TradeDirection == TradeDirection.Long && i0 != CellColor.Red)
                                        || (openIcerSignal.TradeDirection == TradeDirection.Short &&
                                            i0 != CellColor.Green))
                                    {
                                        var signalStepCancel =
                                            new SignalStep
                                            {
                                                SignalId = openIcerSignal.Id,
                                                SignalStepType = SignalStepType.Cancel,
                                                PriceEntry = price,
                                                Reason = "Neues H/T bei ICER=Schwarz"
                                            };
                                        context.SignalSteps.Add(signalStepCancel);
                                        AddSignalStep(signalStepCancel);

                                    }
                                    else if (w0 == (openIcerSignal.TradeDirection == TradeDirection.Long
                                                 ? CellColor.Red
                                                 : CellColor.Green))
                                    {
                                        var reason = "Wolke Farbwechsel";
                                        var signalStepCancel =
                                            new SignalStep
                                            {
                                                SignalId = openIcerSignal.Id,
                                                SignalStepType = SignalStepType.Cancel,
                                                PriceEntry = price,
                                                Reason = reason
                                            };
                                        context.SignalSteps.Add(signalStepCancel);
                                        AddSignalStep(signalStepCancel);
                                        pendingComplete = true;
                                    }

                                    else
                                    {
                                        // sonst werte neu berechnen, neuer signalstep mit change
                                        var r = price.High - price.Low;
                                        var entry =  openIcerSignal.TradeDirection == TradeDirection.Long
                                                ? price.High + (Decimal)0.618 * r
                                                : price.Low - (Decimal)0.618 * r;

                                        var signalStep = new SignalStep
                                        {
                                            Entry = entry,
                                            Sl = openIcerSignal.TradeDirection == TradeDirection.Long
                                                ? price.Low
                                                : price.High,
                                            Tp = openIcerSignal.TradeDirection == TradeDirection.Long
                                                ? entry +(Decimal)2.47 * r
                                                : entry -(Decimal)2.47 * r,
                                            EntryType = EntryType.StopLimit,
                                            ExitType = ExitType.Icer1,
                                            SlType = StopLossType.Icer1,
                                            PriceEntry = price,
                                            SignalStepType = SignalStepType.Change,
                                            SignalId = openIcerSignal.Id,
                                            Reason = "Neues H/T Anpassung der Marken"
                                        };
                                        context.SignalSteps.Add(signalStep);
                                        AddSignalStep(signalStep);


                                    }
                                    pendingComplete = true;
                                }
                                else
                                {
                                    // kein neues H/T checken ob screener noch gut
                                    string reason = null;
                                    if (i0 == (openIcerSignal.TradeDirection == TradeDirection.Long
                                            ? CellColor.Green
                                            : CellColor.Red)) reason = "ICER Farbwechsel";
                                    if (w0 == (openIcerSignal.TradeDirection == TradeDirection.Long
                                            ? CellColor.Red
                                            : CellColor.Green)) reason = "Wolke Farbwechsel";
                                    if (reason != null)
                                    {
                                        var signalStepCancel =
                                            new SignalStep
                                            {
                                                SignalId = openIcerSignal.Id,
                                                SignalStepType = SignalStepType.Cancel,
                                                PriceEntry = price,
                                                Reason = reason
                                            };
                                        context.SignalSteps.Add(signalStepCancel);
                                        AddSignalStep(signalStepCancel);
                                        pendingComplete = true;
                                    }

                                }




                            }

                        }
                        //if (pendingComplete) return;

                        // neue ICER signale in DB erstellen
                        if (newDirection != null)
                        {
                            logger.LogInformation("NewSignal Name: {0}, TS: {1}, Direction: {2}", name,
                                p[0].p.TimeStamp, newDirection);
                            var signal = new Signal
                            {
                                SignalType = SignalType.Icer,
                                TradeDirection = (TradeDirection)newDirection,
                                BrokerInstrumentId = brokerInstrumentId,
                                TimeIntervalId = ti.Id
                            };

                            var r = price.High - price.Low;
                            var signalStep = new SignalStep
                            {
                                Entry = newDirection == TradeDirection.Long
                                    ? price.High + (Decimal)0.618 * r
                                    : price.Low - (Decimal)0.618 * r,
                                Sl = newDirection == TradeDirection.Long ? price.Low : price.High,
                                Tp = newDirection == TradeDirection.Long
                                    ? price.High + (Decimal)0.618 * r + (Decimal)2.47 * (Decimal)1.618 * r
                                    : price.Low - (Decimal)0.618 * r - (Decimal)2.47 * (Decimal)1.618 * r,
                                EntryType = EntryType.StopLimit,
                                ExitType = ExitType.Icer1,
                                PriceEntry = price,
                                SignalStepType = SignalStepType.New,
                                Signal = signal,
                                Reason = "Signalstart"
                            };
                            context.Signals.Add(signal);
                            context.SignalSteps.Add(signalStep);

                            pendingComplete = true;

                        }


// uNL ////////////////////////////////////////////////////////
                        newDirection = null;
                        if(b10 == CellColor.Green && welle0 == CellColor.Red && s0 == CellColor.Red
                          && p[0].ind.FirstOrDefault(y => y.Type == IndicatorDataType.OrigWolke_SpanA).Data >p[0].p.High
                          && p[0].ind.FirstOrDefault(y => y.Type == IndicatorDataType.OrigWolke_SpanB).Data >p[0].p.High)
                            newDirection = TradeDirection.Long;

                        var openUNLs = openSignals.Where(x => x.BrokerInstrumentId == brokerInstrumentId && x.SignalType == SignalType.uNL);            
                        if (openUNLs.Count() > 1)
                        {

                            throw (new Exception("too many open uNL signals, something in the code is wrong"));
                        }  

                        var openuNLSignal = openUNLs.FirstOrDefault(x => x.SignalType == SignalType.uNL);
                        if (openuNLSignal != null)
                        {
                            var lastSignalStep = openuNLSignal.SignalSteps.OrderByDescending(x => x.Updated).First();
                            // ist auch ein neues da? dann laufendes abbrechen
                            if (newDirection != null)
                            {
                                var signalStepCancel =
                                    new SignalStep
                                    {
                                        SignalId = openuNLSignal.Id,
                                        SignalStepType = SignalStepType.Cancel,
                                        PriceEntry = price,
                                        Reason = "neues Signal"
                                    };
                                context.SignalSteps.Add(signalStepCancel);
                                AddSignalStep(signalStepCancel);
                                pendingComplete = true;

                            }
                            else // kein neues da
                            {
                                // neues hoch oder tief
                                if ((openuNLSignal.TradeDirection == TradeDirection.Long &&
                                     lastSignalStep.Sl > price.Low))
                                {
                                    var signalStepCancel =
                                        new SignalStep
                                        {
                                            SignalId = openuNLSignal.Id,
                                            SignalStepType = SignalStepType.Cancel,
                                            PriceEntry = price,
                                            Reason = "Neues Tief unter SL"
                                        };
                                    context.SignalSteps.Add(signalStepCancel);
                                    AddSignalStep(signalStepCancel);
                                }
                            }
                        }

                        if (newDirection != null)
                        {
                            logger.LogInformation("NewSignal uNL Name: {0}, TS: {1}, Direction: {2}", name,
                                p[0].p.TimeStamp, newDirection);
                            var signal = new Signal
                            {
                                SignalType = SignalType.uNL,
                                TradeDirection = (TradeDirection)newDirection,
                                BrokerInstrumentId = brokerInstrumentId,
                                TimeIntervalId = ti.Id
                            };

                            var r = price.High - price.Low;
                            var signalStep = new SignalStep
                            {
                                Entry = price.High,
                                Sl =  price.Low,
                                Tp = price.High + 10 * (price.High - price.Low),
                                EntryType = EntryType.StopLimit,
                                ExitType = ExitType.Icer1,
                                PriceEntry = price,
                                SignalStepType = SignalStepType.New,
                                Signal = signal,
                                Reason = "Signalstart"
                            };
                            context.Signals.Add(signal);
                            context.SignalSteps.Add(signalStep);

                            pendingComplete = true;

                        }

                        if (pendingComplete)
                        {

                            //openSignals = context.Signals.Where(x => x.BrokerInstrumentId == brokerInstrumentId && x.TimeIntervalId == ti.Id)
                            //    .Where(x => x.SignalSteps.All(s => s.SignalStepType != SignalStepType.Cancel)).ToList();
                            pendingComplete = false;

                            break;

                        }

                        // 
                    }
                }
            }
        }

        public Signal DiscardSignal(int signalId)
        {
            var trades = context.Trades
                .Where(x => x.SignalId == signalId)
                .Where(x => x.TradeSteps.All(t => t.Type != TradeStepType.Hide))
                .FirstOrDefault();
            if (trades != null) return null;

            var signal = context.Signals.Include(x => x.SignalSteps).ThenInclude(t => t.PriceEntry)
                .Where(x => x.SignalSteps.All(t => t.SignalStepType != SignalStepType.Cancel))
                .FirstOrDefault(x => x.Id == signalId);

            var lastEntry = context.PriceEntries
                                    .Where(p => p.TimeIntervalId == signal.SignalSteps.First().PriceEntry.TimeIntervalId)
                                    .Where(p => p.BrokerInstrumentId == signal.BrokerInstrumentId)
                                    .OrderByDescending(p => p.TimeStamp)
                                    .FirstOrDefault();
            var newStep = new SignalStep
            {
                SignalId = signalId,
                SignalStepType = SignalStepType.Cancel,
                Reason = "Manuell abgebrochen",
                PriceEntry = lastEntry
            };
            context.SignalSteps.Add(newStep);
            return signal;
        }

        public void UpdateTradeFromSignal(Trade trade, Signal signal = null, bool create = true)
        {
            var entryLevel = signal.SignalSteps.OrderByDescending(s => s.PriceEntry.TimeStamp).FirstOrDefault().Entry;
            var slLevel = signal.SignalSteps.OrderByDescending(s => s.PriceEntry.TimeStamp).FirstOrDefault().Sl;
            var r = Math.Abs((Double)entryLevel - (Double)slLevel);
            var stopLimitBuffer = r / 25;
            var direction = signal.TradeDirection;
            var otherDirection = signal.TradeDirection == TradeDirection.Long ? TradeDirection.Short : TradeDirection.Long;
            var stopLimitLevel = entryLevel > slLevel ? (Double)entryLevel + stopLimitBuffer : (Double)entryLevel - stopLimitBuffer;
            
            var tpLevel = (Decimal)(entryLevel > slLevel ? (Double)entryLevel + 2.47 * r : (Double)entryLevel - 2.47 * r);
            if(trade.Signal.SignalType == SignalType.uNL) tpLevel = (Decimal)entryLevel + 10* (Decimal)(entryLevel-slLevel);


            var size = (int)(this.getR() / r);

            var tradeStep = new TradeStep { Trade = trade, Type = TradeStepType.Prepped, Size = size };
            context.TradeSteps.Add(tradeStep);
            tradeStep.EntryLevel = entryLevel;
            tradeStep.StopLevel = slLevel;
            tradeStep.ProfitLevel = tpLevel;
            var autoExitDate = DateTimeOffset.Now.AddBusinessDays(20);

            Order entryOrder;
            Order tpOrder;
            Order slOrder;
            Order timeExitOrder;

            if (create == false)
            {
                trade.Orders.FirstOrDefault(x => x.State != OrderState.Canceled && x.Function == OrderFunction.Entry).State = OrderState.Canceled;
                trade.Orders.FirstOrDefault(x => x.State != OrderState.Canceled && x.Function == OrderFunction.TP).State = OrderState.Canceled;
                trade.Orders.FirstOrDefault(x => x.State != OrderState.Canceled && x.Function == OrderFunction.SL).State = OrderState.Canceled;
                trade.Orders.FirstOrDefault(x => x.State != OrderState.Canceled && x.Function == OrderFunction.Time).State = OrderState.Canceled;
            }
            entryOrder = new Order { Trade = trade, Function = OrderFunction.Entry, Direction = direction, Type = OrderType.StopLimit, StopLevel = entryLevel, LimitLevel = (Decimal)stopLimitLevel, Size = size, State = OrderState.Prepared };
            tpOrder = new Order { Trade = trade, Function = OrderFunction.TP, Direction = otherDirection, Type = OrderType.Limit, LimitLevel = tpLevel, Size = size, State = OrderState.Prepared };
            slOrder = new Order { Trade = trade, Function = OrderFunction.SL, Direction = otherDirection, Type = OrderType.Stop, StopLevel = slLevel, Size = size, State = OrderState.Prepared };
            timeExitOrder = new Order { Trade = trade, Function = OrderFunction.Time, Direction = otherDirection, Type = OrderType.Market, Size = size, State = OrderState.Prepared, ValidAfter = autoExitDate };
            context.Orders.Add(entryOrder);
            context.Orders.Add(tpOrder);
            context.Orders.Add(slOrder);
            context.Orders.Add(timeExitOrder);
            trade.SignalState = SignalState.Ok;

        }

 

        public Trade CreateTrade(int signalId)
        {
            var signal = context.Signals.Include(x => x.SignalSteps).ThenInclude(t => t.PriceEntry)
                .Where(x => x.SignalSteps.All(t => t.SignalStepType != SignalStepType.Cancel))
                .FirstOrDefault(x => x.Id == signalId);
            if (signal == null) return null;
            var existing = context.Trades.Where(x => x.SignalId == signalId)
                .Where(x => x.TradeSteps.All(t => t.Type != TradeStepType.Cancel))
                .Where(x => x.TradeSteps.All(t => t.Type != TradeStepType.Closed))
                .FirstOrDefault();
            if (existing != null) return null;
            var trade = new Trade { Signal = signal };
            context.Trades.Add(trade);
            UpdateTradeFromSignal(trade, signal);
            this.Complete();
            return trade;
        }

        public void AddSignalStep(SignalStep step)
        {
            var signal = context.Signals
                .Include(s => s.Trades).ThenInclude(x => x.TradeSteps)
                .Include(s => s.Trades).ThenInclude(x => x.Orders)
                .FirstOrDefault(s => s.Id == step.SignalId);

            if (signal.Trades.Count > 0)
            {
                foreach (var trade in signal.Trades)
                {
                    var lastStep = trade.TradeSteps.OrderByDescending(x => x.Created).FirstOrDefault();
                    if(lastStep.Type == TradeStepType.Placed 
                        || lastStep.Type == TradeStepType.Prepped
                        || lastStep.Type == TradeStepType.PrepCancel
                        || lastStep.Type == TradeStepType.PrepChange){
                            if(step.SignalStepType == SignalStepType.Change)
                                trade.SignalState = SignalState.Changed;
                            if(step.SignalStepType == SignalStepType.Cancel)  
                                trade.SignalState = SignalState.Cancelled;
                        }
                      
                    if (lastStep.Type == TradeStepType.Placed && step.SignalStepType == SignalStepType.Change)
                    {
                    //    updateTradeFromSignal(trade, signal, false);
                    }
                }
            }
        }

        public Trade CancelTrade(int tradeId)
        {
            var trade = context.Trades
                .Include(x => x.TradeSteps)
                .Include(x => x.Orders)
                .Include(x => x.Signal).ThenInclude(t => t.SignalSteps)
                .Include(x => x.Signal).ThenInclude(t => t.BrokerInstrument)
                .FirstOrDefault(x => x.Id == tradeId);
            if (trade == null) return null;

            // cancel where nothing happened yet or as confirmation that orders where removed
            if ((trade.TradeSteps.OrderByDescending(x => x.Created).First().Type == TradeStepType.Prepped)
             || (trade.TradeSteps.OrderByDescending(x => x.Created).First().Type == TradeStepType.PrepCancel))
            {
                foreach (var order in trade.Orders) order.State = OrderState.Canceled;
                var newStep = new TradeStep { Trade = trade, Type = TradeStepType.Cancel };
                context.TradeSteps.Add(newStep);
            }

            // cancel where orders really need to be removed from Broker
            if ((trade.TradeSteps.OrderByDescending(x => x.Created).First().Type == TradeStepType.Placed)
                 || (trade.TradeSteps.OrderByDescending(x => x.Created).First().Type == TradeStepType.PrepChange)
                     )
            {
                var newStep = new TradeStep { Trade = trade, Type = TradeStepType.PrepCancel };
                context.TradeSteps.Add(newStep);
            }
            trade.SignalState = SignalState.Ok;
            return trade;
        }
        public Trade ConfirmPlaceTrade(int tradeId, int? size = null, decimal? stop = null, decimal? entry = null, decimal? tp = null)
        {
            var trade = context.Trades
                .Include(x => x.TradeSteps)
                .Include(x => x.Orders)
                .Include(x => x.Signal).ThenInclude(t => t.SignalSteps)
                .Include(x => x.Signal).ThenInclude(t => t.BrokerInstrument)
                .FirstOrDefault(x => x.Id == tradeId);
            if (trade == null) return null;
            var lastStep = trade.TradeSteps.OrderByDescending(x => x.Created).First();

            if ((lastStep.Type == TradeStepType.Prepped)
             || (lastStep.Type == TradeStepType.PrepChange))
            {

                if (size == null) size = lastStep.Size;
                if (stop == null) stop = lastStep.StopLevel;
                if (entry == null) entry = lastStep.EntryLevel;
                if (tp == null) tp = lastStep.ProfitLevel;

                foreach (var order in trade.Orders.Where(x => x.State !=OrderState.Canceled)) order.State = OrderState.Submitted;
                var newStep = new TradeStep { Trade = trade, Type = TradeStepType.Placed, Size = size, EntryLevel = entry, StopLevel = stop, ProfitLevel = tp };
                context.TradeSteps.Add(newStep);
            }

            if ((lastStep.Type == TradeStepType.PrepCancel))
            {
                foreach (var order in trade.Orders) order.State = OrderState.Canceled;
                var newStep = new TradeStep { Trade = trade, Type = TradeStepType.Cancel };
                context.TradeSteps.Add(newStep);
            }
            return trade;
        }

        public Trade ConfirmFilled(int tradeId, decimal executedLevel, int size = 0)
        {
            var trade = context.Trades
                .Include(x => x.TradeSteps)
                .Include(x => x.Orders)
                .Include(x => x.Signal).ThenInclude(t => t.SignalSteps)
                .Include(x => x.Signal).ThenInclude(t => t.BrokerInstrument)
                .FirstOrDefault(x => x.Id == tradeId);
            if (trade == null) return null;

            var lastStep = trade.TradeSteps.OrderByDescending(x => x.Created).First();
            if ((lastStep.Type == TradeStepType.Placed) || (lastStep.Type == TradeStepType.PlacedOld))
            {
                trade.Orders.Where(x => x.State !=OrderState.Canceled).FirstOrDefault(x => x.Function == OrderFunction.Entry).ExecutedLevel = executedLevel;
                trade.Orders.Where(x => x.State !=OrderState.Canceled).FirstOrDefault(x => x.Function == OrderFunction.Entry).State = OrderState.Filled;
                if (size != 0) trade.Orders.Where(x => x.State !=OrderState.Canceled).FirstOrDefault(x => x.Function == OrderFunction.Entry).Size = size;
                var newStep = new TradeStep
                {
                    Trade = trade,
                    Type = TradeStepType.Filled,
                    EntryLevel = executedLevel,
                    StopLevel = lastStep.StopLevel,
                    ProfitLevel = lastStep.ProfitLevel,
                    Size = lastStep.Size
                };
                context.TradeSteps.Add(newStep);
                return trade;
            }

            if ((trade.TradeSteps.OrderByDescending(x => x.Created).First().Type == TradeStepType.Filled)
             || (trade.TradeSteps.OrderByDescending(x => x.Created).First().Type == TradeStepType.TGS))
            {

                var initialEntry = (decimal)trade.Orders.Where(x => x.State !=OrderState.Canceled).FirstOrDefault(x => x.Function == OrderFunction.Entry).StopLevel;
                var einstieg = (decimal)trade.Orders.Where(x => x.State !=OrderState.Canceled).FirstOrDefault(x => x.Function == OrderFunction.Entry).ExecutedLevel;
                var ausstieg = (decimal)executedLevel;
                var initialStop = trade.TradeSteps.OrderBy(x => x.Created).FirstOrDefault(x => x.Type == TradeStepType.Placed).StopLevel;

                var rrr = Math.Abs(initialEntry - (decimal)initialStop);
                var rResult = Math.Abs(ausstieg - einstieg) / rrr;
                OrderFunction func = OrderFunction.SL;
                if (rResult > 1) func = OrderFunction.TP;

                trade.Orders.Where(x => x.State !=OrderState.Canceled).FirstOrDefault(x => x.Function == func).ExecutedLevel = executedLevel;
                trade.Orders.Where(x => x.State !=OrderState.Canceled).FirstOrDefault(x => x.Function == func).State = OrderState.Filled;
                foreach (var order in trade.Orders.Where(x => x.State !=OrderState.Canceled).Where(x => x.Function != func && x.State == OrderState.Submitted))
                    order.State = OrderState.Canceled;

                //if(size!=0) trade.Orders.FirstOrDefault(x => x.Function == OrderFunction.Entry).Size = size;

                var newStep = new TradeStep { Trade = trade, Type = TradeStepType.Closed, ProfitLevel = executedLevel, EntryLevel = lastStep.EntryLevel, Size = lastStep.Size };
                context.TradeSteps.Add(newStep);
                return trade;
            }

            return trade;
        }
        public Trade ConfirmExit(int tradeId, decimal level)
        {
            var trade = context.Trades
                .Include(x => x.TradeSteps)
                .Include(x => x.Orders)
                .Include(x => x.Signal).ThenInclude(t => t.SignalSteps)
                .Include(x => x.Signal).ThenInclude(t => t.BrokerInstrument)
                .FirstOrDefault(x => x.Id == tradeId);
            if (trade == null) return null;
            var lStep = trade.TradeSteps.OrderByDescending(x => x.Created).First();
            if (lStep.Type == TradeStepType.Filled)
            {
                var diff = trade.Orders.FirstOrDefault(x => x.Function == OrderFunction.Entry).ExecutedLevel - level;

                // Take Profit?
                var execFunction = OrderFunction.SL;
                if (diff > 0 && trade.Signal.TradeDirection == TradeDirection.Long || (diff <= 0 && trade.Signal.TradeDirection == TradeDirection.Short))
                    execFunction = OrderFunction.TP;
                var execStepType = TradeStepType.Closed;
                trade.Orders.Where(x => x.State !=OrderState.Canceled).FirstOrDefault(x => x.Function == execFunction).ExecutedLevel = level;
                trade.Orders.Where(x => x.State !=OrderState.Canceled).FirstOrDefault(x => x.Function == execFunction).State = OrderState.Filled;
                foreach (var order in trade.Orders.Where(x => x.State != OrderState.Filled))
                    order.State = OrderState.Canceled;
                var newStep = new TradeStep { Trade = trade, Type = execStepType,EntryLevel = lStep.EntryLevel, ProfitLevel = level};
                context.TradeSteps.Add(newStep);

            }
            return trade;
        }

        public Trade UndoLastTradeStep(int tradeId)
        {
            var trade = context.Trades
                .Include(x => x.TradeSteps)
                .Include(x => x.Orders)
                .FirstOrDefault(x => x.Id == tradeId);
            if (trade == null) return null;
            var step = trade.TradeSteps.OrderByDescending(t => t.Created).FirstOrDefault();
            if (step.Type == TradeStepType.PrepCancel)
                context.TradeSteps.Remove(step);

            return trade;
        }

        public Trade HideTrade(int tradeId)
        {
            var trade = context.Trades
                .Include(x => x.TradeSteps)
                .FirstOrDefault(x => x.Id == tradeId);
            if (trade == null) return null;
            var step = trade.TradeSteps.OrderByDescending(t => t.Created).FirstOrDefault();
            if ((trade.TradeSteps.OrderByDescending(x => x.Created).First().Type == TradeStepType.Cancel)
             || (trade.TradeSteps.OrderByDescending(x => x.Created).First().Type == TradeStepType.Closed))
            {
                var newStep = new TradeStep { Trade = trade, Type = TradeStepType.Hide };
                context.TradeSteps.Add(newStep);
            }
            return trade;
        }

        public Trade UpdateTradeFromSignal(int tradeId)
        {
            var trade = context.Trades.Where(t => t.Id == tradeId)
                        .Include(t=> t.TradeSteps)
                        .Include(t=> t.Orders)
                        .Include(t=> t.Signal)
                        .ThenInclude(t => t.SignalSteps)
                        .ThenInclude(t => t.PriceEntry)
                        .FirstOrDefault();
            if(trade == null) return null;
            var signal = trade.Signal;
            UpdateTradeFromSignal(trade,signal,false);



            return trade;
        }
    }

}
