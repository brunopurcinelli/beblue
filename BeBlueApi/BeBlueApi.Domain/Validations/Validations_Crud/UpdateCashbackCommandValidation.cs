using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class UpdateCashbackCommandValidation : CashbackValidation<UpdateCashbackCommand>
    {
        public UpdateCashbackCommandValidation()
        {
            ValidateId();
            ValidateIdGender();
            ValidatePercent();
            ValidateWeekDay();
        }
    }
}
