using System;
using Auction.WebApp.Domain.ValueTypes;

namespace Auction.WebApp.Domain.EventPayloads
{
    public class AuctionCreated : EventPayload
    {
        public AuctionDetails Auction { get; }

        private AuctionCreated(AuctionDetails auction)
        {
            Auction = auction;
        }

        public static AuctionCreated Create(AuctionDetails auction)
        {
            return new AuctionCreated(auction);
        }
    }
}



