using BeBlueApi.Domain.Validations;

namespace BeBlueApi.Domain.Commands
{
    public class RegisterNewCashbackCommand : CashbackCommand
    {
        public RegisterNewCashbackCommand(string gender, string weekDay, double percent)
        {
            MusicGender = gender;
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
