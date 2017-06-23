using System;
using trading.Models;

namespace trading.Controllers.Resources
{
    public class PriceEntryResource {
        public Decimal Open { get; set; }
        public Decimal High { get; set; }
        public Decimal Low { get; set; }
        public Decimal Close { get; set; }
        public long? Volume { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public bool IsFinished { get; set; } = false;
        public int TimeIntervalId { get; set; }
        public int BrokerInstrumentId { get; set; }
        public virtual TimeInterval TimeInterval { get; set; }
    }
}