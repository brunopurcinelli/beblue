using BeblueApi.Domain.Core.Events;
using System;

namespace BeBlueApi.Domain.Events
{
    public class CashbackRemovedEvent : Event
    {
        public CashbackRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
