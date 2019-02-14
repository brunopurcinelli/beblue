using BeblueApi.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Domain.Models
{
    public class MusicGender : Entity
    {
        public MusicGender(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
        public MusicGender(Guid id, string description, ICollection<Cashback> cashbacks)
        {
            Id = id;
            Description = description;
            Cashbacks = cashbacks;
        }

        // Empty constructor for EF
        protected MusicGender() { }

        public string Description { get; private set; }


        public virtual ICollection<Cashback> Cashbacks { get; private set; }
        public virtual ICollection<DiscMusic> DiscMusics { get; set; }
    }
}
