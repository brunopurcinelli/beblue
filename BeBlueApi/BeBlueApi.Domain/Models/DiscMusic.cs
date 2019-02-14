using BeblueApi.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeBlueApi.Domain.Models
{
    public class DiscMusic : Entity
    {
        public DiscMusic(Guid id, string name, Guid idGender, decimal price)
        {
            Id = id;
            Name = name;
            IdGender = idGender;
            Price = price;
        }

        // Empty constructor for EF
        protected DiscMusic() { }

        public string Name { get; private set; }

        public Guid IdGender { get; set; }

        public decimal Price { get; private set; }
        

        [ForeignKey("IdGender")]
        public virtual MusicGender MusicGender { get; set; }

        public virtual ICollection<SalesLine> SalesLines { get; set; }
    }
}
