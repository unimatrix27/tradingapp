using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class SignalStepRepository : Repository<SignalStep>, ISignalStepRepository
    {
        public SignalStepRepository(TradingDbContext context) : base(context) { }

       
    }
}
