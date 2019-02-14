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
        [JsonProperty("Id do Disco")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do disco é obrigatório")]
        [JsonProperty("Nome do Disco")]
        public string Name { get; set; }

        [Required(ErrorMessage = "É Obrigatório inserir o Gênero Musical")]
        [JsonIgnore]
        public Guid IdGender { get; set; }


        [Required(ErrorMessage = "O Valor é obrigatória")]
        [JsonProperty("Valor")]
        public decimal Price { get; set; }


        [JsonProperty("Gênero Musical")]
        public virtual MusicGenderViewModel MusicGender { get; set; }

        [JsonProperty("Linhas de Venda")]
        public virtual ICollection<SalesLineViewModel> SalesLines { get; set; }
    }
}
