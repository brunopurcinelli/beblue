using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeBlueApi.Application.ViewModels
{
    public class DiscMusicViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome do Disco")]
        public string Name { get; set; }

        [DisplayName("Id do Gênero")]
        public Guid IdGender { get; set; }

        [DisplayName("Valor Unitário")]
        public decimal Price { get; set; }
    }
}
