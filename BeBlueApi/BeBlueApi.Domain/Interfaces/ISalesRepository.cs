using BeBlueApi.Domain.Models;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Domain.Interfaces
{
    public interface ISalesRepository : IRepository<Sales>
    {
        ICollection<Sales> GetAll(int page, int size, DateTime dateInitial, DateTime dateFinal);
    }
}
