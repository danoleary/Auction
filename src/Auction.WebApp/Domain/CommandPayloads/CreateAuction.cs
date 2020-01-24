using Auction.WebApp.Domain.ValueTypes;

namespace Auction.WebApp.Domain.CommandPayloads
{
    public class CreateAuction : CommandPayload
    {
        public AuctionDetails Auction { get; }
 
        private CreateAuction(AuctionDetails auction)
        {
            Auction = auction;
        }

        public static CreateAuction Create(AuctionDetails auction)
        {
            return new CreateAuction(auction);
        }
    }
}



