using System;
using System.Collections.Generic;

namespace trading.Controllers.Resources
{
    public class BrokerResource
    {
       public int Id {get;set;}        
        public DateTimeOffset? Created { get; private set; }
        public DateTimeOffset? Updated { get; private set; }


        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<ExchangeResource> Exchanges { get; set; }

        public BrokerResource(){
             Exchanges = new HashSet<ExchangeResource>();
        }
    }
}