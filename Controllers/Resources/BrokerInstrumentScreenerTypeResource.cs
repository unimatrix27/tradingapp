   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;


namespace trading.Controllers.Resources
{
 
    public partial class BrokerInstrumentScreenerTypeResource 
    {
        public int Id { get; set; }
        public DateTimeOffset? Created { get; private set; }
        public DateTimeOffset? Updated { get; private set; }
        public int BrokerInstrumentId { get; set; }
        public int ScreenerTypeId { get; set; }
        public virtual BrokerInstrumentResource BrokerInstrument { get; set; }




    }
}
