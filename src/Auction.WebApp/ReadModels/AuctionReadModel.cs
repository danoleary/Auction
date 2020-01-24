using System;
using System.Linq;
using System.Collections.Generic;
using Auction.WebApp.Domain;
using Auction.WebApp.Domain.EventPayloads;

namespace Auction.WebApp.ReadModels
{
    public class AuctionReadModel
    {
        public Guid Id { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal HighestBid { get; set; }

        public static AuctionReadModel Build(Guid id, IEnumerable<EventPayload> events)
        {
            return events.Aggregate(new AuctionReadModel(), (agg, evt) => ApplyEvent(id, agg, evt));
        }

        private static AuctionReadModel ApplyEvent(Guid id, AuctionReadModel readModel, EventPayload payload)
        {
            switch (payload)
            {
                case AuctionCreated auctionCreated:
                    return new AuctionReadModel
                    {
                        Id = id,
                        Description = auctionCreated.Auction.Description,
                        EndTime = auctionCreated.Auction.EndTime,
                        StartingPrice = auctionCreated.Auction.StartingPrice,
                        HighestBid = 0.0m
                    };   
                case BidPlaced bidPlaced:
                    return new AuctionReadModel
                    {
                        Id = id,
                        Description = readModel.Description,
                        EndTime = readModel.EndTime,
                        StartingPrice = readModel.StartingPrice,
                        HighestBid = bidPlaced.Bid.Amount
                    };
                default:
                    throw new Exception("Unknown event type");
            }
        }
    }
}
