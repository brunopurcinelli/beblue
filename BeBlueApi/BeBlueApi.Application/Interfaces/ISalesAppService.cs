using BeBlueApi.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Application.Interfaces
{
    public interface ISalesAppService : IDisposable
    {
        void Register(SalesRequest request);
        List<SalesViewModel> GetAll(int page, int size, DateTime dateInitial, DateTime dateFinal);
        SalesViewModel GetById(Guid id);
        void Update(SalesViewModel salesViewModel);
    }
}
