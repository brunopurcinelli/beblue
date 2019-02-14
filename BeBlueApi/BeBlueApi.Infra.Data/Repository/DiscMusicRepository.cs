using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BeBlueApi.Infra.Data.Repository
{
    public class DiscMusicRepository : Repository<DiscMusic>, IDiscMusicRepository
    {
        public DiscMusicRepository(BeblueDbContext context)
            : base(context)
        {

        }

        public ICollection<DiscMusic> GetAll(int pageIndex, int pageSize)
        {
            var records = GetAll().Include("MusicGender").ToList();
            var count = records.Count();

            var list = records.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderBy(o => o.Name).ToList();

            return list;
        }
    }
}
