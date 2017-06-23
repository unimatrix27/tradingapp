   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
 
    public partial class ScreenerLine  : BaseTable
    {
        public ScreenerLine()
        {
            ScreenerEntries = new HashSet<ScreenerEntry>();

        }
        public int BrokerInstrumentId { get; set; }
        public int ScreenerId { get; set; }
        public virtual BrokerInstrument BrokerInstrument { get; set; }
        public virtual Screener Screener { get; set; }

        public virtual ICollection<ScreenerEntry> ScreenerEntries { get; set; }
    }
}
