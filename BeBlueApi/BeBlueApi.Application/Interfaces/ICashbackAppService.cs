using BeBlueApi.Application.EventSourcedNormalizers;
using BeBlueApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Application.Interfaces
{
    public interface ICashbackAppService : IDisposable
    {
        void Register(CashbackViewModel cashbackViewModel);
        IEnumerable<CashbackViewModel> GetAll();
        CashbackViewModel GetById(Guid id);
        void Update(CashbackViewModel cashbackViewModel);
    }
}
