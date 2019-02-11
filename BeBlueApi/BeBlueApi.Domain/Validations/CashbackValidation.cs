using BeBlueApi.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Domain.Validations
{
    public abstract class CashbackValidation<T> : AbstractValidator<T> where T : CashbackCommand
    {
        protected void ValidateMusicGender()
        {
            RuleFor(c => c.MusicGender)
                .NotEmpty().WithMessage("Please ensure you have entered the Music Gender")
                .Length(2, 250).WithMessage("The Name must have between 2 and 250 characters");
        }

        protected void ValidateWeekDay()
        {
            RuleFor(c => c.WeekDay)
                .NotEmpty();
        }

        protected void ValidatePercent()
        {
            RuleFor(c => c.Percent)
                .NotEmpty()
                .Must(HaveMinimumPercent);
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected static bool HaveMinimumPercent(double percent)
        {
            return percent > 0;
        }
    }
}
