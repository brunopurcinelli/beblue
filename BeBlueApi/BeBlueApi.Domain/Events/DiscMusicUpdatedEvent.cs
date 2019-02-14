using BeblueApi.Domain.Core.Events;
using System;

namespace BeBlueApi.Domain.Events
{
    public class DiscMusicUpdatedEvent : Event
    {
        public DiscMusicUpdatedEvent(Guid id, Guid idGender, string name, decimal price)
        {
            Id = id;
            IdGender = idGender;
            Name = name;
            Price = price;
            AggregateId = id;
        }
        public Guid Id { get; set; }
        public string Name { get; private set; }

        public Guid IdGender { get; set; }

        public decimal Price { get; private set; }
    }
}
