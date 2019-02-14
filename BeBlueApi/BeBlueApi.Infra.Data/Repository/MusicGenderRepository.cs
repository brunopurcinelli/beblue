using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.Data.Context;

namespace BeBlueApi.Infra.Data.Repository
{
    public class MusicGenderRepository : Repository<MusicGender>, IMusicGenderRepository
    {
        public MusicGenderRepository(BeblueDbContext context)
            : base(context)
        {

        }
    }
}
