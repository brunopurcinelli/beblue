using System.ComponentModel.DataAnnotations;

namespace BeBlueApi.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Autênticação em Dois Fatores")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Lembrar este computador")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
