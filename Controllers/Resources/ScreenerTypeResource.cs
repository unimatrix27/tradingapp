   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
using trading.Models;

namespace trading.Controllers.Resources
{
 
    public partial class ScreenerTypeResource  
    {
        public ScreenerTypeResource()
        {
            BrokerInstrumentScreenerTypes = new HashSet<BrokerInstrumentScreenerTypeResource>();
        }

        [StringLength(50)]
        public string Name { get; set; }
        public string URL { get; set; }
        public string Path { get; set; }
        public string LastHash { get; set; }
        public string LastResult { get; set; }
        public int? MarkerRed { get; set; }
        public int? MarkerGreen { get; set; }
        public int? MarkerBlue { get; set; }


        public virtual TimeInterval TimeInterval { get; set; }

        [Required]
        public int NameColumn { get; set; }

        public DateTime LastCheck { get; set; }

        public long UpdateFrequencyTicks { get; set; }

        [NotMapped]
        public TimeSpan UpdateFrequency
        {

            get { return new TimeSpan(UpdateFrequencyTicks); }
            set { UpdateFrequencyTicks = value.Ticks; }

        }
        public virtual ICollection<BrokerInstrumentScreenerTypeResource> BrokerInstrumentScreenerTypes { get; set; }
    }
}
