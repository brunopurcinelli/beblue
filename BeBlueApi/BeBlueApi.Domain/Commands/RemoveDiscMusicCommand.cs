using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class RemoveDiscMusicCommand : DiscMusicCommand
    {
        public RemoveDiscMusicCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveDiscMusicCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
