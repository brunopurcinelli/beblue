using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeBlueApi.Application.ViewModels
{
    public class MusicGenderViewModel
    {
        [Key]
        [JsonProperty("Id do Gênero")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "A Descrição do Gênero é Obrigatório")]
        [JsonProperty("Descrição do Gênero")]
        public string Description { get; set; }
    }
}
