using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trading.Controllers.Resources;
using trading.Models;
using trading.Persistence;
using Hangfire;
using trading.Background;

namespace trading.Controllers
{
    public class ScreenersController : Controller
    {
        private readonly TradingDbContext context;
        private readonly IMapper mapper;
        public ScreenersController(TradingDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        [HttpPut("/api/screeners/rescan/{id}")]
        public IActionResult rescanScreener(int id)
        {
            var screener = context.Screeners.FirstOrDefault(x => x.Id == id);
            if (screener == null) return NotFound();
            BackgroundJob.Enqueue<ScreenerParser>(x => x.ParseScreener(JobCancellationToken.Null, id));
            return Ok();
        }

        
        [HttpGet("/api/screeners/last")]
        public IEnumerable<Screener> lastScreeners()
        {
            var types = context.Screeners.Select(x => x.ScreenerTypeId).Distinct();
            List<Screener> result = new List<Screener>();
            foreach (var type in types){
                var screener = context.Screeners.Include(x => x.ScreenerType).OrderByDescending(x => x.TimeStamp).FirstOrDefault(x => x.ScreenerTypeId == type);
                result.Add(screener);
            }
            return result;
        }
    }
}