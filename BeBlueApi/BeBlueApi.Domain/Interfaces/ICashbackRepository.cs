using BeBlueApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Domain.Interfaces
{
    public interface ICashbackRepository : IRepository<Cashback>
    {
        Cashback GetByWeekDay(string weekDay);
    }
}
