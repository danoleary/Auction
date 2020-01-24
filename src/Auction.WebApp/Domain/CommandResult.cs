using System.Collections.Generic;

namespace Auction.WebApp.Domain
{
    public abstract class CommandResult
    {
    }

    public class CommandResultSuccess : CommandResult
    {
        public List<EventPayload> Events { get; }

        private CommandResultSuccess(List<EventPayload> events)
        {
            Events = events;
        }

        public static CommandResultSuccess Create(List<EventPayload> events)
        {
            return new CommandResultSuccess(events);
        } 
    }

    public class CommandResultFailure : CommandResult
    {
        public string Message { get; }

        private CommandResultFailure(string message)
        {
            Message = message;
        }

        public static CommandResultFailure Create(string message)
        {
            return new CommandResultFailure(message);
        } 
    }
}



