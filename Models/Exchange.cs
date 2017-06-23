using System.ComponentModel;

namespace trading.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;

    public partial class Exchange : BaseTable
    {



        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        public int CurrencyId { get; set; }
        [Required]
        public virtual Currency Currency { get; set; }


        [Required]
        public int BrokerId { get; set; }
        public virtual Broker Broker { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string TimeZoneId { get; set; }

        [NotMapped]
        public TimeZoneInfo TimeZone
        {
#pragma warning disable 618
            get { return TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId); }
            set { TimeZoneId = value.StandardName; }
#pragma warning restore 618
        }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        
         public Exchange()
        {
            //BrokerInstruments = new HashSet<BrokerInstrument>();

        }
        public bool IsOpen {get{
                var tzi = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
                DateTimeOffset utcCloseTime = DateTime.SpecifyKind(DateTime.Now.Date + CloseTime- tzi.GetUtcOffset(DateTime.Now), DateTimeKind.Utc);
                if(DateTime.UtcNow < utcCloseTime && (DateTime.Now.DayOfWeek != DayOfWeek.Sunday || DateTime.Now.DayOfWeek != DayOfWeek.Sunday)) return true;
                else return false;
        }}
        public DateTimeOffset LastCloseTime
        { 
            get
            {
                var tzi = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
                DateTimeOffset utcCloseTime = DateTime.SpecifyKind(DateTime.Now.Date + CloseTime- tzi.GetUtcOffset(DateTime.Now), DateTimeKind.Utc);
                if(utcCloseTime > DateTime.Now)
                    utcCloseTime -= TimeSpan.FromDays(1);
                if (utcCloseTime.DayOfWeek == DayOfWeek.Sunday) utcCloseTime -= TimeSpan.FromDays(1);
                if (utcCloseTime.DayOfWeek == DayOfWeek.Saturday) utcCloseTime -= TimeSpan.FromDays(1);
                return utcCloseTime.ToLocalTime();
            }
        }

        public DateTimeOffset getLocalTime(DateTime dateTime)
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            DateTimeOffset utcTime = DateTime.SpecifyKind(dateTime - tzi.GetUtcOffset(dateTime), DateTimeKind.Utc);
            return utcTime.ToLocalTime();
        }

        public DateTimeOffset CloseTimeOnDate(DateTime date)
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            DateTimeOffset utcCloseTime = DateTime.SpecifyKind(date.Date + CloseTime - tzi.GetUtcOffset(date), DateTimeKind.Utc);
            return utcCloseTime.ToLocalTime();
        }


 
        //public virtual ICollection<BrokerInstrument> BrokerInstruments { get; set; }
    }
}
