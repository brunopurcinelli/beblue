using BeBlueApi.Domain.Validations;

namespace BeBlueApi.Domain.Commands
{
    public class RegisterNewMusicGenderCommand : MusicGenderCommand
    {
        public RegisterNewMusicGenderCommand(string description)
        {
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewMusicGenderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
