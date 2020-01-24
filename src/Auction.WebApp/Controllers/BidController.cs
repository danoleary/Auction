using System;
using Microsoft.AspNetCore.Mvc;
using Auction.WebApp.Domain;
using Auction.WebApp.Dtos;
using Auction.WebApp.Domain.CommandPayloads;
using Auction.WebApp.Domain.ValueTypes;
using Newtonsoft.Json;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    public class BidController : Controller
    {
        private CommandHandler _commandHandler { get; }

        public BidController(EventStore eventStore)
        {
            _commandHandler = CommandHandler.Create(eventStore);
        }

        [HttpPost]
        public ActionResult Place([FromBody]PlaceBidDto dto)
        {
            Console.WriteLine("Bid: " + JsonConvert.SerializeObject(dto));
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var bid = Bid.Create(dto.Amount);
            var placeBid = PlaceBid.Create(bid);
            var command = Command.Create(dto.Id, placeBid);
            var result = _commandHandler.HandleCommand(command);
            switch (result)
            {
                case CommandResultSuccess _:
                    return Ok();
                default:
                    return Conflict();
            }
        }
    }
}
