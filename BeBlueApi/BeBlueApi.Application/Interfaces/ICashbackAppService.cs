using BeBlueApi.Application.EventSourcedNormalizers;
using BeBlueApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Application.Interfaces
{
    public interface ICashbackAppService : IDisposable
    {
        void Register(CashbackViewModel CashbackViewModel);
        IEnumerable<CashbackViewModel> GetAll();
        CashbackViewModel GetById(Guid id);
        void Update(CashbackViewModel CashbackViewModel);
        void Remove(Guid id);
        IList<CashbackHistoryData> GetAllHistory(Guid id);
    }
}
