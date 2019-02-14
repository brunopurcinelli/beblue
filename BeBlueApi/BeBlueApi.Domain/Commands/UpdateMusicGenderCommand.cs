using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class UpdateMusicGenderCommand : MusicGenderCommand
    {
        public UpdateMusicGenderCommand(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateMusicGenderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
