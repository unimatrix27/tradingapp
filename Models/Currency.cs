using System.ComponentModel;

namespace trading.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;

    public partial class Currency : BaseTable
    {

        [Required]
        [StringLength(3)]
        public string Name { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exchange> Exchanges { get; set; }

        public Currency()
        {
       //     BrokerSymbols = new HashSet<BrokerSymbol>();

                Exchanges = new HashSet<Exchange>();
        }
    }
}
