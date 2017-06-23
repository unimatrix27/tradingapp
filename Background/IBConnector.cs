using Hangfire;
using Trading.Persistence.Interfaces;
using IBApi;


using System.Threading;
using System;



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

            while(true){
            Thread.Sleep(2000);
            Console.WriteLine(DateTime.Now);
            }

        }


        public void Connect()
        {
            
        }
    }
}

