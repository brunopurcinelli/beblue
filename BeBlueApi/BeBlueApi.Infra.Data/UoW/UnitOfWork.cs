using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BeblueDbContext _context;

        public UnitOfWork(BeblueDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
