using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trading.Controllers.Resources;
using trading.Models;
using trading.Persistence;
using Trading.Persistence;

namespace trading.Controllers
{
    public class BrokersController : Controller
    {
        private readonly UnitOfWork uow;
        private readonly IMapper mapper;
        public BrokersController(UnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;

        }

        [HttpGetAttribute("/api/brokers")]
        public async Task<IEnumerable<BrokerResource>> GetBrokers()
        {
            var brokers = uow.Brokers.GetAll();
            return mapper.Map<IEnumerable<Broker>,IEnumerable<BrokerResource>>(brokers);
        }
    }
}