using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class UpdateCashbackCommand : CashbackCommand
    {
        public UpdateCashbackCommand(Guid id, string gender, string weekDay, double percent)
        {
            Id = id;
            MusicGender = gender;
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
