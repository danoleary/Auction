using System;
using System.Collections.Generic;
using System.Linq;

namespace Auction.WebApp.Domain
{
    public class EventStore
    {
        private IEnumerable<Event> _events { get; set; }

        private EventStore()
        {
            _events = new List<Event>();
        }

        public static EventStore Create()
        {
            return new EventStore();
        }

        public IEnumerable<Event> GetEvents(Guid aggregateId) =>
            _events.Where(x => x.AggregateId == aggregateId);

        public IEnumerable<Event> GetEvents() => _events;

        public void StoreEvents(IEnumerable<Event> events)
        {
            _events = _events.Concat(events);
        }
    }
}



