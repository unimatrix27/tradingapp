using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggers;

namespace trading.Models
{

    public partial class TimeInterval : BaseTable
    {



        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public long DurationTicks { get; set; }

        [NotMapped]
        public TimeSpan Duration
        {
            get { return new TimeSpan(DurationTicks); }
            set { DurationTicks = value.Ticks; }
        }
        public TimeInterval()
        {
            //BrokerTimeIntervals = new HashSet<BrokerTimeInterval>();
            //PriceEntries = new HashSet<PriceEntry>();
            Triggers<TimeInterval>.Inserting += entry => entry.Entity.Created = entry.Entity.Updated = DateTimeOffset.Now;
            Triggers<TimeInterval>.Updating += entry => entry.Entity.Updated = DateTimeOffset.Now;
        }
        public virtual ICollection<BrokerTimeInterval> BrokerTimeIntervals { get; set; }


        //public virtual ICollection<PriceEntry> PriceEntries { get; set; }

    }
}
