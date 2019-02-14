using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class RemoveMusicGenderCommand : MusicGenderCommand
    {
        public RemoveMusicGenderCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveMusicGenderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
