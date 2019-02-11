using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BeBlueApi.Infra.Data.Repository
{
    public class CashbackRepository : Repository<Cashback>, ICashbackRepository
    {
        public CashbackRepository(BeblueDbContext context)
            : base(context)
        {

        }

        public Cashback GetByWeekDay(string weekDay)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.WeekDay == weekDay);
        }
    }
}
