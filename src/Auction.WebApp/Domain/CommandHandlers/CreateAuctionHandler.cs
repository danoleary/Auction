using System.Collections.Generic;
using Auction.WebApp.Domain.CommandPayloads;
using Auction.WebApp.Domain.EventPayloads;

namespace Auction.WebApp.Domain.CommandHandlers
{
    public class CreateAuctionHandler
    {
        public static CommandResult Handle(State state, CreateAuction createAuction)
        {
            switch (state)
            {
                case EmptyState _:
                    var auctionCreated = AuctionCreated.Create(createAuction.Auction);
                    return CommandResultSuccess.Create(new List<EventPayload> { auctionCreated });
                default:
                    return CommandResultFailure.Create("Auction already created");
            }
        }
    }
}



