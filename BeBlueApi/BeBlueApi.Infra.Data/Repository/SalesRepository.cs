using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeBlueApi.Infra.Data.Repository
{
    public class SalesRepository : Repository<Sales>, ISalesRepository
    {
        public SalesRepository(BeblueDbContext context)
            : base(context)
        {

        }
        public ICollection<Sales> GetAll(int page, int size, DateTime dateInitial, DateTime dateFinal)
        {
            var records = GetAll().Where(w=>w.SalesDate >= dateInitial && w.SalesDate <= dateFinal).Include("Lines").ToList();
            var count = records.Count();

            var list = records.Skip((page - 1) * size).Take(size).OrderByDescending(o => o.SalesDate).ToList();

            return list;
        }
    }
}
