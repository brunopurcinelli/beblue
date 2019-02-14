using BeBlueApi.Domain.Commands;
using FluentValidation;
using System;

namespace BeBlueApi.Domain.Validations
{
    public abstract class DiscMusicValidation<T> : AbstractValidator<T> where T : DiscMusicCommand
    {
        protected void ValidateIdGender()
        {
            RuleFor(c => c.IdGender)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty();
        }

        protected void ValidatePrice()
        {
            RuleFor(c => c.Price)
                .NotEmpty()
                .Must(HaveMinimumPrice);
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected static bool HaveMinimumPrice(decimal price)
        {
            return price > 0;
        }
    }
}
