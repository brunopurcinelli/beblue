using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RegisterNewCashbackCommandValidation : CashbackValidation<RegisterNewCashbackCommand>
    {
        public RegisterNewCashbackCommandValidation()
        {
            ValidateMusicGender();
            ValidatePercent();
            ValidateWeekDay();
        }
    }
}
