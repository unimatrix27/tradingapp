   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
 
    public partial class ScreenerType  : BaseTable
    {
        public ScreenerType()
        {
            ScreenerEntryMappings= new HashSet<ScreenerEntryMapping>();
            Screeners = new HashSet<Screener>();
            ScreenerReferenceImages = new HashSet<ScreenerReferenceImage>();
            BrokerInstrumentScreenerTypes = new HashSet<BrokerInstrumentScreenerType>();
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
        public bool UseToday { get; set; }

        [NotMapped]
        public TimeSpan UpdateFrequency
        {

            get { return new TimeSpan(UpdateFrequencyTicks); }
            set { UpdateFrequencyTicks = value.Ticks; }

        }
        public virtual ICollection<ScreenerEntryMapping> ScreenerEntryMappings { get; set; }
        public virtual ICollection<Screener> Screeners { get; set; }
        public virtual ICollection<ScreenerReferenceImage> ScreenerReferenceImages { get; set; }
        public virtual ICollection<BrokerInstrumentScreenerType> BrokerInstrumentScreenerTypes { get; set; }
    }
}
