using System;

namespace trading.Controllers.Resources
{
    public class ExchangeResource
    {
        public int Id {get;set;}        
        public DateTimeOffset? Created { get; private set; }
        public DateTimeOffset? Updated { get; private set; }

        public string Name { get; set; }

        public int CurrencyId { get; set; }
        public virtual CurrencyResource Currency { get; set; }

        public int BrokerId { get; set; }
        public bool IsOpen { get; set; }

        public string TimeZoneId { get; set; }

        public TimeZoneInfo TimeZone{ get; set; }

        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
                
        public DateTimeOffset LastCloseTime { get; private set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerSymbol> BrokerSymbols { get; set; }

    }
}