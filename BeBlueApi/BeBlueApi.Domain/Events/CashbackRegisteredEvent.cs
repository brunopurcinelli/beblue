using BeblueApi.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Domain.Events
{
    public class CashbackRegisteredEvent : Event
    {
        public CashbackRegisteredEvent(Guid id, Guid idGender, string weekDay, decimal percent)
        {
            Id = id;
            IdGender = idGender;
            WeekDay = weekDay;
            Percent = percent;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public Guid IdGender { get; private set; }

        public string WeekDay { get; private set; }

        public decimal Percent { get; private set; }
    }
}
