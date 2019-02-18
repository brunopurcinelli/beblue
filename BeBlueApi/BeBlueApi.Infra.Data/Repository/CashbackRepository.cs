using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BeBlueApi.Infra.Data.Repository
{
    public class CashbackRepository : Repository<Cashback>, ICashbackRepository
    {
        public CashbackRepository(BeblueDbContext context)
            : base(context)
        {

        }

        public Cashback GetByWeekDay(Guid idGender, DateTime date)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.IdGender.ToString() == idGender.ToString() && c.WeekDay == date.DayOfWeek);
        }
    }
}
