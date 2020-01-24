using System;

namespace Auction.WebApp.Domain.CommandPayloads
{
    public class CreateAuction : CommandPayload
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public decimal StartingPrice { get; }
        public string Username { get; }

        private CreateAuction(DateTime startTime, DateTime endTime, decimal startingPrice, string userName)
        {
            StartTime = startTime;
            EndTime = endTime;
            StartingPrice = startingPrice;
            Username = userName;
        }

        public static CreateAuction Create(DateTime startTime, DateTime endTime, decimal startingPrice, string userName)
        {
            return new CreateAuction(startTime, endTime, startingPrice, userName);
        }
    }
}



