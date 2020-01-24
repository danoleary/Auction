using Auction.WebApp.Domain.ValueTypes;

namespace Auction.WebApp.Domain.EventPayloads
{
    public class BidPlaced : EventPayload
    {
        public Bid Bid { get; }

        private BidPlaced(Bid bid)
        {
            Bid = bid;
        }

        public static BidPlaced Create(Bid bid)
        {
            return new BidPlaced(bid);
        }
    }
}



