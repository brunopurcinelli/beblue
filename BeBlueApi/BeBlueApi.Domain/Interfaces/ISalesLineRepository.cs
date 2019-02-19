using System;
using System.Collections.Generic;
using BeBlueApi.Domain.Models;

namespace BeBlueApi.Domain.Interfaces
{
    public interface ISalesLineRepository : IRepository<SalesLine>
    {
        List<SalesLine> GetBySalesId(Guid id);
    }
}
