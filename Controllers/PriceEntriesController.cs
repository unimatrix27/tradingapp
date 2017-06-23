using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trading.Controllers.Resources;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace trading.Controllers
{ 
    public class PriceEntriesController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public PriceEntriesController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;

        }

        [HttpGetAttribute("/api/pricesfull")]
        public  IEnumerable<object> GetFull([FromQuery] string screener,  int brokerInstrumentId, int back,  int count, string timeInterval = "D1")
        {

            return uow.GetAllWithIndicatorAndScreenerFlat(screener, brokerInstrumentId, back, count,
                timeInterval);
            //return mapper.Map<IEnumerable<Broker>, IEnumerable<BrokerResource>>(brokers);
        }
    }
}