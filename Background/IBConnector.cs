using Hangfire;
using Trading.Persistence.Interfaces;
using IBApi;

namespace trading.Background
{
    public class IbConnector
    {
        private IUnitOfWork uow;
        private Contract contract;


        public IbConnector() { 
            
        }

        public void Start(IJobCancellationToken cancellationToken)
        {
            Connect();


        }


        public void Connect()
        {
            
        }
    }
}

