using BeblueApi.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Domain.Models
{
    public class Cashback : Entity
    {
        public Cashback(Guid id, string gender, string weekDay, double percent)
        {
            Id = id;
            MusicGender = gender;
            WeekDay = weekDay;
            Percent = percent;
        }

        // Empty constructor for EF
        protected Cashback() { }

        public string MusicGender { get; private set; }

        public string WeekDay { get; private set; }

        public double Percent { get; private set; }
    }
}
