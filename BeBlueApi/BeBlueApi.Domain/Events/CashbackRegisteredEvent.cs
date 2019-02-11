using BeblueApi.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Domain.Events
{
    public class CashbackRegisteredEvent : Event
    {
        public CashbackRegisteredEvent(Guid id, string musicGender, string weekDay, double percent)
        {
            Id = id;
            MusicGender = musicGender;
            WeekDay = weekDay;
            Percent = percent;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string MusicGender { get; private set; }

        public string WeekDay { get; private set; }

        public double Percent { get; private set; }
    }
}
