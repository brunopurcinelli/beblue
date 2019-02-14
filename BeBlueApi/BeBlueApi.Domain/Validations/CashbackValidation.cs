using BeBlueApi.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Domain.Validations
{
    public abstract class CashbackValidation<T> : AbstractValidator<T> where T : CashbackCommand
    {
        protected void ValidateIdGender()
        {
            RuleFor(c => c.IdGender)
                .NotEqual(Guid.Empty);
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

        protected static bool HaveMinimumPercent(decimal percent)
        {
            return percent > 0;
        }
    }
}
