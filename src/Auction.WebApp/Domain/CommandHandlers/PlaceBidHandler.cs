using System.Collections.Generic;
using Auction.WebApp.Domain.CommandPayloads;
using Auction.WebApp.Domain.EventPayloads;
using System;

namespace Auction.WebApp.Domain.CommandHandlers
{
    public class PlaceBidHandler
    {
        public static CommandResult Handle(State state, PlaceBid placeBid)
        {
            switch (state)
            {
                case CreatedState created:
                    if(DateTime.UtcNow >= created.EndTime)
                    {
                        CommandResultFailure.Create("Auction has finished");
                    }
                    if(placeBid.Bid.Amount <= created.HighestBid)
                    {
                        CommandResultFailure.Create("Bid is not higher than current highest bid");
                    }
                    var bidPlaced = BidPlaced.Create(placeBid.Bid);
                    return CommandResultSuccess.Create(new List<EventPayload> { bidPlaced });
                default:
                    return CommandResultFailure.Create("Auction not created");
            }
        }
    }
}



