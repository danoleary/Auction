using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Auction.WebApp.Domain;
using Auction.WebApp.ReadModels;
using Auction.WebApp.Dtos;
using Auction.WebApp.Domain.CommandPayloads;
using Auction.WebApp.Domain.ValueTypes;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    public class AuctionController : Controller
    {
        private EventStore _eventStore { get; }
        private CommandHandler _commandHandler { get; }

        public AuctionController(EventStore eventStore)
        {
            _eventStore = eventStore;
            _commandHandler = CommandHandler.Create(eventStore);
        }

        [HttpPost]
        public ActionResult Create([FromBody]CreateAuctionDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var aggregateId = Guid.NewGuid();
            var auction = AuctionDetails.Create(DateTime.UtcNow.AddMinutes(1), dto.StartingPrice, dto.Description);
            var createAuction = CreateAuction.Create(auction);
            var command = Command.Create(aggregateId, createAuction);
            var result = _commandHandler.HandleCommand(command);
            switch (result)
            {
                case CommandResultSuccess _:
                    return Ok();
                default:
                    return Conflict();
            }
        }

        [HttpGet("live")]
        public IEnumerable<AuctionReadModel> LiveAuctions()
        {
            return GetAuctions().Where(x => DateTime.UtcNow < x.EndTime);
        }

        [HttpGet("finished")]
        public IEnumerable<AuctionReadModel> FinishedAuctions()
        {
            return GetAuctions().Where(x => DateTime.UtcNow >= x.EndTime);
        }

        private IEnumerable<AuctionReadModel> GetAuctions()
        {
            return _eventStore.GetEvents()
                .GroupBy(x => x.AggregateId)
                .Select(x => AuctionReadModel.Build(x.Key, x.Select(e => e.Payload)));
        }
    }
}
