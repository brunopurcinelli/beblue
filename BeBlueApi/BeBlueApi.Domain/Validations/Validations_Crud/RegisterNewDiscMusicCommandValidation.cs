using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RegisterNewDiscMusicCommandValidation : DiscMusicValidation<RegisterNewDiscMusicCommand>
    {
        public RegisterNewDiscMusicCommandValidation()
        {
            ValidateIdGender();
            ValidateName();
            ValidatePrice();
        }
    }
}
