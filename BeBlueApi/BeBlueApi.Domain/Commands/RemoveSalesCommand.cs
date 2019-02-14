using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class RemoveSalesCommand : SalesCommand
    {
        public RemoveSalesCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveSalesCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
