namespace Auction.WebApp.Domain.ValueTypes
{
    public class Bid
    {
        public decimal Amount { get; }

        private Bid(decimal amount)
        {
            Amount = amount;
        }

        public static Bid Create(decimal amount)
        {
            return new Bid(amount);
        }
    }
}
