using System;
using System.Collections.Generic;

namespace trading.Controllers.Resources
{
    public class InstrumentNameResource
    {
        public int Id {get;set;}        
        public DateTimeOffset? Created { get; private set; }
        public DateTimeOffset? Updated { get; private set; }

        public string Name { get; set; }
        public virtual ICollection<BrokerSymbolResource> BrokerSymbols { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerSymbol> BrokerSymbols { get; set; }
        public InstrumentNameResource()
        {
                BrokerSymbols = new HashSet<BrokerSymbolResource>();

        }
    }
}