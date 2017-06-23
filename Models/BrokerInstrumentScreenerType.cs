   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
 
    public partial class BrokerInstrumentScreenerType  : BaseTable
    {
        public int BrokerInstrumentId { get; set; }
        public int ScreenerTypeId { get; set; }
        public virtual BrokerInstrument BrokerInstrument { get; set; }
        public virtual ScreenerType ScreenerType { get; set; }



    }
}
