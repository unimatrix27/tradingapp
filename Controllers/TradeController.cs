using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using trading.Controllers.Resources;
using trading.Models;
using Trading.Persistence.Interfaces;

namespace trading.Controllers
{
    [Route("/api/trades")]
    public class TradeController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public TradeController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }

        [HttpGet]
        public IEnumerable<TradeResource> GetTrades()
        {
            var trades = uow.Trades.GetAllFull();
            trades = trades.Where(x => x.TradeSteps.OrderByDescending(t => t.Created).FirstOrDefault().Type != TradeStepType.Hide);
            var result = mapper.Map<IEnumerable<Trade>, IEnumerable<TradeResource>>(trades);
            return result;
        }

        
        [HttpPost("Discard")]
        public IActionResult DiscardSignal([FromBody] int signalId)
        {
            var result = uow.DiscardSignal(signalId);
            if(result == null){
                return StatusCode((int) HttpStatusCode.Conflict); 
            }
            uow.Complete();
            var res2 = uow.Trades.GetAllFull().FirstOrDefault(x => x.Id == result.Id);
            // var result = mapper.Map<InstrumentName, InstrumentNameResource>(instrumentName);
            return Ok(mapper.Map<Trade,TradeResource>(res2));
        }      
        
        [HttpPost("Create")]
        public IActionResult CreateTrade([FromBody] int signalId)
        {
            var result = uow.CreateTrade(signalId);
            if(result == null){
                return StatusCode((int) HttpStatusCode.Conflict); 
            }
            uow.Complete();
            var res2 = uow.Trades.GetAllFull().FirstOrDefault(x => x.Id == result.Id);
            // var result = mapper.Map<InstrumentName, InstrumentNameResource>(instrumentName);
            return Ok(mapper.Map<Trade,TradeResource>(res2));
        }

        [SwaggerResponse("200", typeof(TradeResource))]
        [HttpPost("Cancel")]
        public IActionResult CancelTrade([FromBody] int tradeId)
        {
            var result = uow.CancelTrade(tradeId);
            if(result == null){
                return StatusCode((int) HttpStatusCode.BadRequest); 
            }
            uow.Complete();
            var res2 = uow.Trades.GetAllFull().FirstOrDefault(x => x.Id == result.Id);
            // var result = mapper.Map<InstrumentName, InstrumentNameResource>(instrumentName);
            return Ok(res2);
        }

        [SwaggerResponse("200", typeof(TradeResource))]
        [HttpPut("Update")]
        public IActionResult UpdateTradeFromSignal([FromBody] int tradeId)
        {
            var result = uow.UpdateTradeFromSignal(tradeId);
            if(result == null){
                return StatusCode((int) HttpStatusCode.BadRequest); 
            }
            uow.Complete();
            var res2 = uow.Trades.GetAllFull().FirstOrDefault(x => x.Id == result.Id);
            // var result = mapper.Map<InstrumentName, InstrumentNameResource>(instrumentName);
            return Ok(res2);
        }  

        [SwaggerResponse("200", typeof(TradeResource))]
        [HttpPost("Undo")]
        public IActionResult UndoLastTradeStep([FromBody] int tradeId)
        {
            var result = uow.UndoLastTradeStep(tradeId);
            if(result == null){
                return StatusCode((int) HttpStatusCode.BadRequest); 
            }
            uow.Complete();
            var res2 = uow.Trades.GetAllFull().FirstOrDefault(x => x.Id == result.Id);
            // var result = mapper.Map<InstrumentName, InstrumentNameResource>(instrumentName);
            return Ok(res2);
        }

        [HttpPost("Hide")]
        public IActionResult Hide([FromBody] int tradeId)
        {
            var result = uow.HideTrade(tradeId);
            if(result == null){
                return StatusCode((int) HttpStatusCode.BadRequest); 
            }
            uow.Complete();
            var res2 = uow.Trades.GetAllFull().FirstOrDefault(x => x.Id == result.Id);
            // var result = mapper.Map<InstrumentName, InstrumentNameResource>(instrumentName);
            return Ok(res2);
        }

        [HttpPost("Confirm")]
        public IActionResult ConfirmPlaceTrade([FromBody] int tradeId)
        {
            var result = uow.ConfirmPlaceTrade(tradeId);
            if(result == null){
                return StatusCode((int) HttpStatusCode.BadRequest); 
            }
            uow.Complete();
            var res2 = uow.Trades.GetAllFull().FirstOrDefault(x => x.Id == result.Id);
            // var result = mapper.Map<InstrumentName, InstrumentNameResource>(instrumentName);
            return Ok(res2);
        }

        [HttpPost("Filled")]
        public IActionResult FilledTrade([FromBody] int tradeId, decimal executedLevel)
        {
            var result = uow.ConfirmFilled(tradeId, executedLevel);
            if(result == null){
                return StatusCode((int) HttpStatusCode.BadRequest); 
            }
            uow.Complete();
            var res2 = uow.Trades.GetAllFull().FirstOrDefault(x => x.Id == result.Id);
            // var result = mapper.Map<InstrumentName, InstrumentNameResource>(instrumentName);
            return Ok(res2);
        }
    }
}