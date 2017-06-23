using System;
using System.IO;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using trading.Persistence;
using System.Linq;
using System.Net;
using trading.Models;
using Microsoft.Extensions.Logging;
using ImageMagick;
using Trading.Persistence;
using Trading.Persistence.Interfaces;

namespace trading.Background
{
    public class ScreenerParseScheduler
    {
        private IUnitOfWork uow;


        public ScreenerParseScheduler(IUnitOfWork uow) { 
            this.uow = uow;
        }

        public void Schedule(IJobCancellationToken cancellationToken)
        {
            string id="";
            //var result = _context.Screeners
            //                 .Where(x => x.IsProcessed == false && x.ParseError == true).ToList();
            var result = uow.Screeners.GetAll().Where(x => x.IsProcessed == false);
            if (result == null) return;


            foreach (var screener in result){
              BackgroundJob.Enqueue<ScreenerParser>(x => x.ParseScreener(JobCancellationToken.Null, screener.Id));
            }

            //foreach (var screener in result)
            // {
            //    id = id == "" ? BackgroundJob.Enqueue<ScreenerParser>(x => x.ParseScreener(JobCancellationToken.Null, screener.id))
            //                  : BackgroundJob.ContinueWith<ScreenerParser>(id, x => x.ParseScreener(JobCancellationToken.Null, screener.id));
            //}
           // BackgroundJob.Enqueue<ScreenerParser>(x => x.ParseScreener(JobCancellationToken.Null,4111));
            //BackgroundJob.Enqueue<ScreenerParser>(x => x.ParseScreener(JobCancellationToken.Null,1051));

        }
    }
}

