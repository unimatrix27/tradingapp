using System.Linq;
using AutoMapper;
using trading.Controllers.Resources;
using trading.Models;

namespace trading.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<InstrumentName,InstrumentNameResource>();
            CreateMap<Currency,CurrencyResource>();
            CreateMap<Exchange,ExchangeResource>();
            CreateMap<Broker,BrokerResource>();
            CreateMap<InstrumentType,InstrumentTypeResource>();
            CreateMap<BrokerSymbol,BrokerSymbolResource>();
            CreateMap<PriceEntry, PriceEntryResource>();
            CreateMap<BrokerInstrument, BrokerInstrumentResource>()
            .ForMember(bir => bir.BrokerName, opt => opt.MapFrom(bi => bi.BrokerSymbol.Exchange.Broker.Name))
            .ForMember(bir => bir.CurrencyName, opt => opt.MapFrom(bi => bi.BrokerSymbol.Exchange.Currency.Name))
            .ForMember(bir => bir.ExchangeName, opt => opt.MapFrom(bi => bi.BrokerSymbol.Exchange.Name))
            .ForMember(bir => bir.InstrumentNameName, opt => opt.MapFrom(bi => bi.BrokerSymbol.InstrumentName.Name))
            .ForMember(bir => bir.TypeName, opt => opt.MapFrom(bi => bi.InstrumentType.Name))
            .ForMember(bir => bir.SymbolName, opt => opt.MapFrom(bi => bi.BrokerSymbol.Name));
            CreateMap<ScreenerReferenceImage,ScreenerReferenceImageResource>();
            CreateMap<ScreenerType,ScreenerTypeResource>();
            CreateMap<BrokerInstrumentScreenerType,BrokerInstrumentScreenerTypeResource>();

            CreateMap<BrokerInstrument,InstrumentNameAllResource>()
            .ForMember(x => x.Id,opt => opt.MapFrom(bi => bi.BrokerSymbol.InstrumentNameId))
            .ForMember(x => x.BrokerInstrumentId,opt => opt.MapFrom(bi => bi.Id))
            .ForMember(x => x.BrokerSymbolId,opt => opt.MapFrom(bi => bi.BrokerSymbolId))
            .ForMember(x => x.BrokerSymbol, opt => opt.MapFrom(bi => bi.BrokerSymbol.Name))
            .ForMember(x => x.Name, opt => opt.MapFrom(bi => bi.BrokerSymbol.InstrumentName.Name))
            .ForMember(x => x.Broker,opt => opt.MapFrom(bi => bi.BrokerSymbol.Exchange.Broker.Name))
            .ForMember(x => x.Exchange,opt => opt.MapFrom(bi => bi.BrokerSymbol.Exchange.Name))
            .ForMember(x => x.Currency,opt => opt.MapFrom(bi => bi.BrokerSymbol.Exchange.Currency.Name))
            .ForMember(x => x.Type,opt => opt.MapFrom(bi => bi.InstrumentType.Name))
            .ForMember(x => x.Screener, opt => opt.MapFrom(bi => bi.BrokerInstrumentScreenerTypes.FirstOrDefault().ScreenerType.Name))
            .ForMember(x => x.lastPriceEntry,opt => opt.MapFrom(bi => bi.PriceEntries.OrderByDescending(p => p.TimeStamp).FirstOrDefault()));

            CreateMap<Signal, SignalResource>()
                .ForMember(x => x.Instrument, opt => opt.MapFrom(bi => bi.BrokerInstrument))
                .MaxDepth(2); 

            CreateMap<SignalStep, SignalStepResource>().MaxDepth(2); 

            CreateMap<Trade, TradeResource>()
                .MaxDepth(2);
            CreateMap<Order, OrderResource>().MaxDepth(2);
            CreateMap<TradeStep, TradeStepResource>().MaxDepth(2);

        }
    }
}
    