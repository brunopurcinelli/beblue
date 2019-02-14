using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class RegisterNewCashbackCommand : CashbackCommand
    {
        public RegisterNewCashbackCommand(Guid idGender, string weekDay, decimal percent)
        {
            IdGender = idGender;
            WeekDay = weekDay;
            Percent = percent;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCashbackCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
