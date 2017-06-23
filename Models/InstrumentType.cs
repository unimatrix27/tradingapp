namespace trading.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;

    public partial class InstrumentType : BaseTable
    {


                [Required]
        [StringLength(20)]
        public string Name { get; set; }
       // public virtual ICollection<BrokerInstrument> BrokerInstruments { get; set; }
        public InstrumentType()
        {
            //BrokerInstruments = new HashSet<BrokerInstrument>();

        }
    }
}
