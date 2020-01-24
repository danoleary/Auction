using System;

namespace Auction.WebApp.Dtos
{
    public class PlaceBidDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
    }
}
