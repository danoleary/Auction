using System.ComponentModel.DataAnnotations;

namespace Auction.WebApp.Dtos
{
    public class CreateAuctionDto
    {

        [Required]
        public decimal StartingPrice { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
