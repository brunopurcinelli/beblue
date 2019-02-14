using BeBlueApi.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Application.Interfaces
{
    public interface ISalesLineAppService : IDisposable
    {
        void Register(SalesLineViewModel salesLineViewModel);
        IEnumerable<SalesLineViewModel> GetAll();
        SalesLineViewModel GetById(Guid id);
        void Update(SalesLineViewModel salesLineViewModel);
        void Remove(Guid id);
    }
}
