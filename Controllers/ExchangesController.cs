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
    public class ExchangesController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ExchangesController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;

        }

        [HttpGetAttribute("/api/exchanges")]
        public  IEnumerable<ExchangeResource> GetExchanges()
        {
            var exchanges = uow.Exchanges.GetAllWithCurrency();
            return mapper.Map<IEnumerable<Exchange>, IEnumerable<ExchangeResource>>(exchanges);
        }
    }
}