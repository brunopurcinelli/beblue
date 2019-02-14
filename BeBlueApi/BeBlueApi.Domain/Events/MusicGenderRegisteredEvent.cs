using BeblueApi.Domain.Core.Events;
using System;

namespace BeBlueApi.Domain.Events
{
    public class MusicGenderRegisteredEvent : Event
    {
        public MusicGenderRegisteredEvent(Guid id, string description)
        {
            Id = id;
            Description = description;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Description { get; private set; }
    }
}
