using System;

namespace trading.Controllers.Resources
{
    public class CurrencyResource
    {
        public int Id {get;set;}        
        public DateTimeOffset? Created { get; private set; }
        public DateTimeOffset? Updated { get; private set; }
        public string Name { get; set; }

        public decimal Rate { get; set; }



    }
}