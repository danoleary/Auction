using System;

namespace Auction.WebApp.Domain
{
    public abstract class State
    { }


    public class EmptyState : State
    {
        private EmptyState()
        {

        }

        public static EmptyState Create()
        {
            return new EmptyState();
        }
    }

    public class CreatedState : State
    {
        public DateTime EndTime { get; }
        public decimal StartingPrice { get; }
        public decimal HighestBid { get;  }


        private CreatedState(DateTime endTime, decimal startingPrice, decimal highestBid)
        {
            EndTime = endTime;
            StartingPrice = startingPrice;
            HighestBid = highestBid;
        }

        public static CreatedState Create(DateTime endTime, decimal startingPrice)
        {
            return new CreatedState(endTime, startingPrice, 0m);
        }

        public static CreatedState WithHighestBid(CreatedState state, decimal amount)
        {
            return new CreatedState(state.EndTime, state.StartingPrice, amount);
        }
    }
}
