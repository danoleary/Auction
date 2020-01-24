using System;

namespace Auction.WebApp.Domain.ValueTypes
{
	public class AuctionDetails
    {
		public DateTime EndTime { get; }
		public decimal StartingPrice { get; }
        public string Description { get;  }

		private AuctionDetails(DateTime endTime, decimal startingPrice, string description)
		{
			EndTime = endTime;
			StartingPrice = startingPrice;
            Description = description;

        }

		public static AuctionDetails Create(DateTime endTime, decimal startingPrice, string  description)
		{
			return new AuctionDetails(endTime, startingPrice, description);
		}
	}
}



