using System;
using System.Linq;
using Xunit;
using Auction.WebApp.Domain;
using Auction.WebApp.Domain.CommandPayloads;
using Auction.WebApp.Domain.EventPayloads;
using Auction.WebApp.Domain.ValueTypes;

namespace Auction.WebApp.UnitTests.Domain
{
    public class WhenHandlingPlaceBid
    {
        private CommandHandler _subject;
        private Guid _aggregateId;
        private static AuctionDetails _auction = AuctionDetails.Create(DateTime.Now.AddDays(7), 10.00m, "Playstation", "Dan");
        private static CreateAuction _createAuction = CreateAuction.Create(_auction);
        private static Bid _bid = Bid.Create(11.0m, "User2");
        private static PlaceBid _placeBid = PlaceBid.Create(_bid);

        public WhenHandlingPlaceBid()
        {
            _subject = CommandHandler.Create(EventStore.Create());
            _aggregateId = Guid.NewGuid();
        }

        [Fact]
        public void IfThereAreNoOtherBidsThenBidPlacedIsRaised()
        {
            var createAuctionCommand = Command.Create(_aggregateId, _createAuction);
            _subject.HandleCommand(createAuctionCommand);
            var placeBidCommand = Command.Create(_aggregateId, _placeBid);
            var result = _subject.HandleCommand(placeBidCommand);
            Assert.True(result is CommandResultSuccess);
            var newEvent = (result as CommandResultSuccess).Events.Single();
            Assert.True(newEvent is BidPlaced);
            Assert.Equal(11.0m, (newEvent as BidPlaced).Bid.Amount);
            Assert.Equal("User2", (newEvent as BidPlaced).Bid.Username);
        }

        [Fact]
        public void IfBidIsHigherThanPreviousBidsThenBidPlacedIsRaised()
        {
            var createAuctionCommand = Command.Create(_aggregateId, _createAuction);
            _subject.HandleCommand(createAuctionCommand);
            var placeBidCommand = Command.Create(_aggregateId, _placeBid);
            _subject.HandleCommand(placeBidCommand);
            var result = _subject.HandleCommand(Command.Create(_aggregateId, PlaceBid.Create(Bid.Create(11.5m, "User3"))));
            Assert.True(result is CommandResultSuccess);
            var newEvent = (result as CommandResultSuccess).Events.Single();
            Assert.True(newEvent is BidPlaced);
            Assert.Equal(11.5m, (newEvent as BidPlaced).Bid.Amount);
            Assert.Equal("User3", (newEvent as BidPlaced).Bid.Username);
        }

        [Fact]
        public void IfThereIsNoAuctionStartedThenCommandFails()
        {
            var placeBidCommand = Command.Create(_aggregateId, _placeBid);
            var result = _subject.HandleCommand(placeBidCommand);
            Assert.True(result is CommandResultFailure);
        }

        //TODO more test on behaviour here
    }
}
