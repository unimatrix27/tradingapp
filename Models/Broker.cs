   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
 
    public partial class Broker : BaseTable
    {


        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(2)]
        public string ShortName { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerSymbol> BrokerSymbols { get; set; }

        public Broker()
        {
       //     BrokerSymbols = new HashSet<BrokerSymbol>();

                // BrokerLogins = new HashSet<BrokerLogin>();

            // PickupTimes = new HashSet<PickupTime>();
            Exchanges = new HashSet<Exchange>();
            // BrokerTimeIntervals = new HashSet<BrokerTimeInterval>();
        }

       
        // public virtual ICollection<BrokerLogin> BrokerLogins { get; set; }

      
        // public virtual ICollection<PickupTime> PickupTimes { get; set; }
        public virtual ICollection<Exchange> Exchanges { get; set; }
        // public virtual ICollection<BrokerTimeInterval> BrokerTimeIntervals { get; set; }
    }
}
