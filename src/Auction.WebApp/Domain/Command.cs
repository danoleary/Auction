using System;

namespace Auction.WebApp.Domain
{
    public class Command
    {
        public Guid AggregateId { get; }
        public CommandPayload Payload { get; }

        private Command(Guid aggregateId, CommandPayload payload)
        {
            AggregateId = aggregateId;
            Payload = payload;
        }

        public static Command Create(Guid aggregateId, CommandPayload payload)
        {
            return new Command(aggregateId, payload);
        }
    }

    public abstract class CommandPayload {}
}



