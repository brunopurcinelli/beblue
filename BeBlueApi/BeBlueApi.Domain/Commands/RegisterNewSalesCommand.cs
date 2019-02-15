using BeBlueApi.Domain.Validations;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Domain.Commands
{
    public class RegisterNewSalesCommand : SalesCommand
    {
        public RegisterNewSalesCommand(DateTime salesDate)
        {
            SalesDate = salesDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewSalesCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
