using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeBlueApi.Infra.Data.Repository
{
    public class SalesLineRepository : Repository<SalesLine>, ISalesLineRepository
    {
        public SalesLineRepository(BeblueDbContext context)
            : base(context)
        {

        }
        public virtual List<SalesLine> GetBySalesId(Guid id)
        {
            var records = Db.SalesLine.Where(w => w.IdSales == id).ToList();
            return records;
        }

    }
}
