   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
 
    public partial class BrokerTimeInterval  : BaseTable
    {
        [Required]
        public int TimeIntervalId { get; set; }
        public int BrokerId { get; set; }
        public virtual TimeInterval TimeInterval { get; set; }

        [Required]
        public virtual Broker Broker { get; set; }

        [Required]
        [StringLength(20)]
        public string BrokerName { get; set; }
        
        public int dataPriority { get; set; }
    }
}
