using BeBlueApi.Domain.Commands;
using FluentValidation;
using System;

namespace BeBlueApi.Domain.Validations
{
    public abstract class SalesValidation<T> : AbstractValidator<T> where T : SalesCommand
    {
        protected void ValidateSalesDate()
        {
            RuleFor(c => c.SalesDate)
                .NotEmpty();
        }

        protected void ValidateTotalAmount()
        {
            RuleFor(c => c.TotalAmount)
                .NotEmpty();
        }
        

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
