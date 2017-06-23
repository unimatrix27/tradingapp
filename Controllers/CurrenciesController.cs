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
    public class CurrenciesController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CurrenciesController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;

        }

        [HttpGetAttribute("/api/currencies")]
        public IEnumerable<CurrencyResource> GetCurrencies()
        {
            var currencies = uow.Currencies.GetAll();
            return mapper.Map<IEnumerable<Currency>, IEnumerable<CurrencyResource>>(currencies);
        }
    }
}