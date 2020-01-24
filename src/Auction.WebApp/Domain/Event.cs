using System;

namespace Auction.WebApp.Domain
{
    public class Event
    {
        public Guid AggregateId { get; }
        public EventPayload Payload { get; }

        private Event(Guid aggregateId, EventPayload payload)
        {
            AggregateId = aggregateId;
            Payload = payload;
        }

        public static Event Create(Guid aggregateId, EventPayload payload)
        {
            return new Event(aggregateId, payload);
        }
    }

    public abstract class EventPayload {}
}



