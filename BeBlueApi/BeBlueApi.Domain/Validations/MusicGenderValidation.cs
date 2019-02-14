using BeBlueApi.Domain.Commands;
using FluentValidation;
using System;

namespace BeBlueApi.Domain.Validations
{
    public abstract class MusicGenderValidation<T> : AbstractValidator<T> where T : MusicGenderCommand
    {
        protected void ValidateDescription()
        {
            RuleFor(c => c.Description)
                .NotEmpty();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected static bool HaveMinimumPercent(decimal percent)
        {
            return percent > 0;
        }
    }
}
