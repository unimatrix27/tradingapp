namespace trading.Controllers.Resources
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;

    public class BrokerSymbolResource 
    {
        public int Id { get; set; }
        public DateTimeOffset? Created { get; private set; }
        public DateTimeOffset? Updated { get; private set; }
        public int InstrumentNameId { get; set; }

        public string Name { get; set; }

        public virtual BrokerResource Broker { get; set; }
        //public virtual InstrumentNameResource InstrumentName { get; set; }
        public virtual ICollection<BrokerInstrumentResource> BrokerInstruments { get; set; }

        public BrokerSymbolResource()
        {
            BrokerInstruments = new HashSet<BrokerInstrumentResource>();
        }
    }
}
