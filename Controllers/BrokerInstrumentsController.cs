using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trading.Controllers.Resources;
using trading.Models;
using Trading.Persistence.Interfaces;

namespace trading.Controllers
{
    [Route("/api/brokerinstruments")]
    public class BrokerInstrumentsController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public BrokerInstrumentsController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }


        [HttpGet("screener/{id}")]
        public IEnumerable<BrokerInstrumentResource> GetBrokerInstrumentsForScreener(int id)
        {
            var brokerInstruments = uow.BrokerInstruments.GetAllWithNames()
                .Where(i => i.BrokerInstrumentScreenerTypes.Any(st => st.ScreenerTypeId == id));

            return mapper.Map<IEnumerable<BrokerInstrument>, IEnumerable<BrokerInstrumentResource>>(brokerInstruments);
        }

        [HttpGet]
        public IEnumerable<InstrumentNameAllResource> GetBrokerInstruments()
        {
            var brokerInstruments = uow.BrokerInstruments.GetAllWithNames().ToList();
            //return mapper.Map<List<BrokerInstrument>,List<BrokerInstrumentResource>>(brokerInstruments);
            var inresource = mapper.Map<List<BrokerInstrument>, List<InstrumentNameAllResource>>(brokerInstruments);
            return inresource;
        }

    }
}