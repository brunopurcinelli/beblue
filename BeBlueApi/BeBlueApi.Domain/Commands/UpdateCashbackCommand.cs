using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class UpdateCashbackCommand : CashbackCommand
    {
        public UpdateCashbackCommand(Guid id, Guid idGender, string weekDay, decimal percent)
        {
            Id = id;
            IdGender = idGender;
            WeekDay = weekDay;
            Percent = percent;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCashbackCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
