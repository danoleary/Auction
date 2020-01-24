using System;
using System.Linq;
using Xunit;
using Auction.WebApp.Domain;
using Auction.WebApp.Domain.CommandPayloads;
using Auction.WebApp.Domain.EventPayloads;
using Auction.WebApp.Domain.ValueTypes;

namespace Auction.WebApp.UnitTests.Domain
{
    public class WhenHandlingStartAuction
    {
        private CommandHandler _subject;
        private Guid _aggregateId;
        private static AuctionDetails _auction = AuctionDetails.Create(DateTime.Now.AddDays(7), 10.00m, "Guitar", "Dan");
        private static CreateAuction _createAuction = CreateAuction.Create(_auction);

        public WhenHandlingStartAuction()
        {
            _subject = CommandHandler.Create(EventStore.Create());
            _aggregateId = Guid.NewGuid();
        }

        [Fact]
        public void IfThereAreNoOtherEventsThenAuctionCreatedIsRaised()
        {
            var createAuctionCommand = Command.Create(_aggregateId, _createAuction);
            var result = _subject.HandleCommand(createAuctionCommand);
            Assert.True(result is CommandResultSuccess);
            var newEvent = (result as CommandResultSuccess).Events.Single();
            Assert.True(newEvent is AuctionCreated);
            Assert.Equal(_auction.EndTime, (newEvent as AuctionCreated).Auction.EndTime);
            Assert.Equal(_auction.StartingPrice, (newEvent as AuctionCreated).Auction.StartingPrice);
            Assert.Equal(_auction.Username, (newEvent as AuctionCreated).Auction.Username);
        }

        [Fact]
        public void IfThereAreIsAlreadyAnAuctionCreatedThenCommandFails()
        {
            var createAuctionCommand = Command.Create(_aggregateId, _createAuction);
            _subject.HandleCommand(createAuctionCommand);
            var result = _subject.HandleCommand(createAuctionCommand);
            Assert.True(result is CommandResultFailure);
        }
    }
}
