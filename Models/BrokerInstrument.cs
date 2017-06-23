namespace trading.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;

    public partial class BrokerInstrument : BaseTable
    {
       

        public int BrokerSymbolId { get; set; }

        public int InstrumentTypeId { get; set; }

        [StringLength(50)]
        public string expiry { get; set; }

        public int? multiplicator { get; set; }


        public  InstrumentType InstrumentType { get; set; }

        public  BrokerSymbol BrokerSymbol { get; set; }
        public BrokerInstrument()
        {
            PriceEntries = new HashSet<PriceEntry>();
            BrokerInstrumentScreenerTypes = new HashSet<BrokerInstrumentScreenerType>();
            ScreenerLines = new HashSet<ScreenerLine>();
        }
        
        public virtual ICollection<PriceEntry> PriceEntries { get; set; }
        public virtual ICollection<BrokerInstrumentScreenerType> BrokerInstrumentScreenerTypes { get; set; }
        public virtual ICollection<ScreenerLine> ScreenerLines { get; set; }


    }
}
