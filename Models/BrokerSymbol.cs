namespace trading.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;

    public partial class BrokerSymbol : BaseTable
    {

        public int ExchangeId { get; set; }
        public int InstrumentNameId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual Exchange Exchange { get; set; }
        public virtual InstrumentName InstrumentName { get; set; }
        public virtual ICollection<BrokerInstrument> BrokerInstruments { get; set; }

        public BrokerSymbol()
        {
            BrokerInstruments = new HashSet<BrokerInstrument>();

        }
    }
}
