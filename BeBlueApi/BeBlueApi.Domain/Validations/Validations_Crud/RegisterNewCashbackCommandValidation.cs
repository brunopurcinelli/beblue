using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RegisterNewCashbackCommandValidation : CashbackValidation<RegisterNewCashbackCommand>
    {
        public RegisterNewCashbackCommandValidation()
        {
            ValidateIdGender();
            ValidatePercent();
            ValidateWeekDay();
        }
    }
}
