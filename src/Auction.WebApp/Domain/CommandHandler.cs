using System;
using System.Linq;
using Auction.WebApp.Domain.CommandPayloads;
using Auction.WebApp.Domain.EventPayloads;
using Auction.WebApp.Domain.CommandHandlers;

namespace Auction.WebApp.Domain
{
    public class CommandHandler
    {
        private EventStore _eventStore { get; }

        private CommandHandler(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public static CommandHandler Create(EventStore eventStore)
        {
            return new CommandHandler(eventStore);
        }

        public CommandResult HandleCommand(Command command)
        {
            var events = _eventStore.GetEvents(command.AggregateId).Select(x => x.Payload);
            var state = events.Aggregate(EmptyState.Create() as State, (agg, evt) => ApplyEvent(agg, evt));
            var result = HandleCommand(state, command.Payload);
            if (result is CommandResultSuccess)
            {
                var newEvents =
                    ((CommandResultSuccess)result).Events.Select(x => Event.Create(command.AggregateId, x));
                _eventStore.StoreEvents(newEvents);
            }
            return result;
        }

        private CommandResult HandleCommand(State state, CommandPayload payload)
        {
            switch (payload)
            {
                case CreateAuction createAuction:
                    return CreateAuctionHandler.Handle(state, createAuction);
                case PlaceBid placeBid:
                    return PlaceBidHandler.Handle(state, placeBid);
                default:
                    return CommandResultFailure.Create("Unknown command payload type");

            }
        }

        private State ApplyEvent(State state, EventPayload payload)
        {
            switch(payload)
            {
                case AuctionCreated auctionCreated:
                    return CreatedState.Create(
                        auctionCreated.Auction.EndTime,
                        auctionCreated.Auction.StartingPrice);
                case BidPlaced bidPlaced:
                    return CreatedState.WithHighestBid(state as CreatedState, bidPlaced.Bid.Amount);
                default:
                    throw new Exception("Unknown event type");
            }
        }
    }

    
}



