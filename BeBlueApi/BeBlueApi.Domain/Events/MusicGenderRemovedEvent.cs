using BeblueApi.Domain.Core.Events;
using System;

namespace BeBlueApi.Domain.Events
{
    public class MusicGenderRemovedEvent : Event
    {
        public MusicGenderRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public Guid Id { get; set; }
    }
}
