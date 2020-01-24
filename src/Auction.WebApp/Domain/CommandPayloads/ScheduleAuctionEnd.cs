using System;

namespace Auction.WebApp.Domain.CommandPayloads
{
    public class StartAuction : CommandPayload
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public decimal StartingPrice { get; }
        public string Username { get; }

        private StartAuction(DateTime startTime, DateTime endTime, decimal startingPrice, string userName)
        {
            StartTime = startTime;
            EndTime = endTime;
            StartingPrice = startingPrice;
            Username = userName;
        }

        public static StartAuction Create(DateTime startTime, DateTime endTime, decimal startingPrice, string userName)
        {
            return new StartAuction(startTime, endTime, startingPrice, userName);
        }
    }
}



