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
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É Obrigatório inserir o Gênero Musical")]
        [MinLength(2)]
        [MaxLength(250)]
        [DisplayName("Gênero Musical")]
        public string MusicGender { get; set; }

        [Required(ErrorMessage = "O dia da Semana é Obrigatório")]
        [EmailAddress]
        [DisplayName("Dia da Semana")]
        public string WeekDay { get; set; }

        [Required(ErrorMessage = "A porcentagem do Cashback é obrigatória")]
        [DisplayName("Porcentagem")]
        public double Percent { get; set; }
    }
}
