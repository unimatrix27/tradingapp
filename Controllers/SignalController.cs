using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using trading.Controllers.Resources;
using trading.Models;
using Trading.Persistence.Interfaces;

namespace trading.Controllers
{
    [Route("/api/signals/")]
    public class SignalController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public SignalController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }

        [HttpGet]
        public IEnumerable<SignalResource> GetOpen()
        {
            var signals = uow.Signals.GetOpen();
            var result = mapper.Map<IEnumerable<Signal>, IEnumerable<SignalResource>>(signals);
            return result;
        }
    }
}