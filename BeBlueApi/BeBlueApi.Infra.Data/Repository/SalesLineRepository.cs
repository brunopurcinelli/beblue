using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.Data.Context;

namespace BeBlueApi.Infra.Data.Repository
{
    public class SalesLineRepository : Repository<SalesLine>, ISalesLineRepository
    {
        public SalesLineRepository(BeblueDbContext context)
            : base(context)
        {

        }
    }
}
