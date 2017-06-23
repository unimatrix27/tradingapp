using System;
using System.Collections.Generic;
using trading.Models;

namespace trading.Controllers.Resources
{

    public class BrokerInstrumentResource
    {
       
        public int Id {get;set;}
        public DateTimeOffset? Created { get; private set; }
        public DateTimeOffset? Updated { get; private set; }
        public int BrokerSymbolId { get; set; }

        public int InstrumentTypeId { get; set; }

        public int ExchangeId { get; set; }

        public string expiry { get; set; }

        public int? multiplicator { get; set; }
        public virtual InstrumentTypeResource InstrumentType { get; set; }
        public string ExchangeName{ get; set; }
        public string BrokerName{ get; set; }
        public string TypeName{ get; set; }
         public string SymbolName{ get; set; }
        public string CurrencyName{ get; set; }
        public string InstrumentNameName{ get; set; }

        public int InstrumentNameId{ get; set; }
        //public virtual ICollection<BrokerInstrumentScreenerTypeResource> BrokerInstrumentScreenerTypes  { get; set; }

        public BrokerInstrumentResource()
        {
            //BrokerInstrumentScreenerTypes = new HashSet<BrokerInstrumentScreenerTypeResource>();
            //PriceEntries = new HashSet<PriceEntry>();
            //ScreenerLines = new HashSet<ScreenerLine>();

        }
        
        //public virtual ICollection<PriceEntry> PriceEntries { get; set; }
        //public virtual ICollection<ScreenerLine> ScreenerLines { get; set; }


    }
}
