using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class RemoveSalesLineCommand : SalesLineCommand
    {
        public RemoveSalesLineCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveSalesLineCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
