using System.ComponentModel.DataAnnotations;

namespace trading.Controllers.Resources
{

    public class InstrumentNameAllResource
    {
        public int Id {get;set;}        
        public int BrokerInstrumentId {get;set;}  
        public int BrokerSymbolId {get;set;}  

        public string Name { get; set; }
        public string BrokerSymbol { get; set; }
        public string Broker { get; set; }
        public string Exchange { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public string Expiry { get; set; }
        public string Screener { get; set; }
        public PriceEntryResource lastPriceEntry {get;set;}
        public int? Multiplicator { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerSymbol> BrokerSymbols { get; set; }

    }
}