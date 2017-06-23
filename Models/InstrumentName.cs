
namespace trading.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;

    public partial class InstrumentName : BaseTable
    {
        

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<BrokerSymbol> BrokerSymbols { get; set; }

        public InstrumentName()
        {
                BrokerSymbols = new HashSet<BrokerSymbol>();

        }
    }  
}   