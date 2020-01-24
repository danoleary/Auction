using Auction.WebApp.Domain.ValueTypes;

namespace Auction.WebApp.Domain.CommandPayloads
{
    public class PlaceBid : CommandPayload
    {
        public Bid Bid { get; }

        private PlaceBid(Bid bid)
        {
            Bid = bid;
        }

        public static PlaceBid Create(Bid bid)
        {
            return new PlaceBid(bid);
        }
    }
}



