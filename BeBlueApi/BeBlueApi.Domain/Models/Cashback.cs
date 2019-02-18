using BeblueApi.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeBlueApi.Domain.Models
{
    public class Cashback : Entity
    {
        public Cashback(Guid id, Guid idGender, DayOfWeek weekDay, decimal percent)
        {
            Id = id;
            IdGender = idGender;
            WeekDay = weekDay;
            Percent = percent;
        }

        // Empty constructor for EF
        protected Cashback() { }

        public Guid IdGender { get; private set; }

        public DayOfWeek WeekDay { get; private set; }

        public decimal Percent { get; private set; }
        
        [ForeignKey("IdGender")]
        public virtual MusicGender MusicGender{ get; set; }
    }
}
