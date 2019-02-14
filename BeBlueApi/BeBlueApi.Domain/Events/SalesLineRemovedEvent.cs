using BeblueApi.Domain.Core.Events;
using System;

namespace BeBlueApi.Domain.Events
{
    public class SalesLineRemovedEvent : Event
    {
        public SalesLineRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id { get; set; }
    }
}
