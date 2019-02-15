using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class UpdateSalesCommand : SalesCommand
    {
        public UpdateSalesCommand(Guid id, decimal totalAmount, decimal totalCashback)
        {
            Id = id;
            TotalAmount = totalAmount;
            TotalCashback = totalCashback;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateSalesCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
