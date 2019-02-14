using BeBlueApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeBlueApi.Domain.Interfaces
{
    public interface IDiscMusicRepository : IRepository<DiscMusic>
    {
        ICollection<DiscMusic> GetAll(int pageIndex, int pageSize);
    }
}
