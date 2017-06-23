using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trading.Models;

namespace Trading.Persistence.Transfer
{
    public class PriceEntryWithIndications
    {
        public PriceEntry p { get; set; }
        public IEnumerable<IndicatorEntry> ind { get; set; }
        public IEnumerable<ScreenerEntry> scr { get; set; }
    }


}
