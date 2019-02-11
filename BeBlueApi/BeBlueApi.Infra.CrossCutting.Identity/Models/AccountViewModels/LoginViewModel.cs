using System.ComponentModel.DataAnnotations;

namespace BeBlueApi.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Continuar logado?")]
        public bool RememberMe { get; set; }
    }
}
