using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trading.Controllers.Resources;
using trading.Models;
using trading.Persistence;

namespace trading.Controllers
{
    public class InstrumentTypesController : Controller
    {
        private readonly TradingDbContext context;
        private readonly IMapper mapper;
        public InstrumentTypesController(TradingDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        [HttpGetAttribute("/api/instrumenttypes")]
        public async Task<IEnumerable<InstrumentTypeResource>> GetInstrumentTypes()
        {
            var instrumenttypes = await context.InstrumentTypes.ToListAsync();
            return mapper.Map<List<InstrumentType>,List<InstrumentTypeResource>>(instrumenttypes);
        }
    }
}