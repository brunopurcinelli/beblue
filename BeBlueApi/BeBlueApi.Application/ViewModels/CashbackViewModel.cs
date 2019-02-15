using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeBlueApi.Application.ViewModels
{
    public class CashbackViewModel
    {
        [Key]
        [JsonProperty("Id do Cashback")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É Obrigatório inserir o Gênero Musical")]
        [JsonIgnore]
        public Guid IdGender { get; set; }

        [Required(ErrorMessage = "O dia da Semana é Obrigatório")]
        [JsonProperty("Dia da Semana")]
        public string WeekDay { get; set; }

        [Required(ErrorMessage = "A porcentagem do Cashback é obrigatória")]
        [JsonProperty("Porcentagem")]
        public decimal Percent { get; set; }
    }
}
