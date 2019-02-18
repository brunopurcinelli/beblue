using BeBlueApi.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeBlueApi.Domain.Commands
{
    public class RegisterNewSalesCommand : SalesCommand
    {
        public RegisterNewSalesCommand(List<LineCommand> salesLines)
        {
            SalesDate = DateTime.Now.ToLocalTime();
            SalesLines = salesLines;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewSalesCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
