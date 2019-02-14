using BeBlueApi.Domain.Commands;
using FluentValidation;
using System;

namespace BeBlueApi.Domain.Validations
{
    public abstract class SalesLineValidation<T> : AbstractValidator<T> where T : SalesLineCommand
    {
        protected void ValidateIdSales()
        {
            RuleFor(c => c.IdSales)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateIdItem()
        {
            RuleFor(c => c.IdItem)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateQuantity()
        {
            RuleFor(c => c.Quantity)
                .NotEqual(0);
        }

        protected void ValidatePriceUnit()
        {
            RuleFor(c => c.PriceUnit)
                .NotEqual(0);
        }

        protected void ValidateSalesPrice()
        {
            RuleFor(c => c.SalesPrice)
                .NotEqual(0);
        }

        protected void ValidateCashback()
        {
            RuleFor(c => c.Cashback)
                .NotEqual(0);
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
