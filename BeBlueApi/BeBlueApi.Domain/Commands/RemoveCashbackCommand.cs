using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class RemoveCashbackCommand : CashbackCommand
    {
        public RemoveCashbackCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCashbackCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
