using Hangfire;
using Trading.Persistence.Interfaces;
using IBApi;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
using System.Threading;
using System;
>>>>>>> Stashed changes
=======
using System.Threading;
using System;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
<<<<<<< Updated upstream

=======
=======
>>>>>>> Stashed changes
            while(true){
            Thread.Sleep(2000);
            Console.WriteLine(DateTime.Now);
            }
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

        }


        public void Connect()
        {
            
        }
    }
}

