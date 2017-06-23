using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trading.Controllers.Resources;
using trading.Models;
using trading.Persistence;
using System;
using Hangfire;
using trading.Background;

namespace trading.Controllers
{

    [Route("/api/refimages")]
    public class RefImageController : Controller
    {
        private readonly TradingDbContext context;
        private readonly IMapper mapper;
        public RefImageController(TradingDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstrumentName(int id, [FromBody] int  BrokerInstrumentId)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            var existingReferenceImage = await context.ScreenerReferenceImages.FindAsync(id);
            if (existingReferenceImage == null)
            {
                return NotFound();
            }
            if (BrokerInstrumentId == 0) existingReferenceImage.BrokerInstrumentId = null;
            else existingReferenceImage.BrokerInstrumentId = BrokerInstrumentId;

            await context.SaveChangesAsync();
            if(getCount()==0){
                BackgroundJob.Schedule<ScreenerParseScheduler>(x => x.Schedule(JobCancellationToken.Null),TimeSpan.FromSeconds(1));
            }

            var result = mapper.Map<ScreenerReferenceImage, ScreenerReferenceImageResource>(existingReferenceImage);
            return Ok(result);


        }

        [HttpGet("next")]
        public IActionResult nextRefImage(){
            Random rnd = new Random();
            var referenceImages = context.ScreenerReferenceImages
                .Where(x => x.BrokerInstrumentId == null && x.Unused == false)
                .Include(x => x.ScreenerType)
                .ThenInclude(st => st.BrokerInstrumentScreenerTypes)
                .ThenInclude(bi => bi.BrokerInstrument)
                .ThenInclude(bist => bist.BrokerSymbol)
                .ThenInclude(bin => bin.InstrumentName)
                .OrderBy(item => rnd.Next());
            var referenceImage = referenceImages.FirstOrDefault();
            if(referenceImage == null) return NotFound();
            var returnImage = mapper.Map<ScreenerReferenceImage, ScreenerReferenceImageResource>(referenceImage);
            returnImage.UnreferencedImages = referenceImages.Count();
            return Ok(returnImage);
        }

        [HttpGet]
        public IActionResult getRefImages(){
            var referenceImages = context.ScreenerReferenceImages
                .Where(x => x.BrokerInstrumentId != null && x.Unused == false)
                .Include(st => st.ScreenerType)
                .Include(bi => bi.BrokerInstrument)
                .ThenInclude(bist => bist.BrokerSymbol)
                .ThenInclude(bin => bin.InstrumentName)
                .Include(bi => bi.BrokerInstrument)
                .ThenInclude(t => t.InstrumentType)
                .ToList();
            if(referenceImages == null) return NotFound();
            var returnImages = mapper.Map<List<ScreenerReferenceImage>, List<ScreenerReferenceImageResource>>(referenceImages);
            return Ok(returnImages);
        }


        [HttpGet("/refimages/show/{imageId}")]
        public ActionResult Show(int imageId)
        {
            string basedir = @"\\192.168.2.2\familie\07_Trading\newstructure\data\ref\";
            ScreenerReferenceImage myImage;  
            myImage = context.ScreenerReferenceImages.FirstOrDefault(x => x.Id == imageId);
            if(myImage == null)
                return new EmptyResult();

            var path = string.Concat(basedir,myImage.ImageSignature,".gif");
            return new FileStreamResult(new FileStream(path, FileMode.Open), "image/gif");
        }

        [HttpGet("count")]
        public int getCount(){
            var referenceImages = context.ScreenerReferenceImages
                .Where(x => x.BrokerInstrumentId == null).Count();
            return referenceImages;
        }

    }
}