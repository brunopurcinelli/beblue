using BeBlueApi.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Application.Interfaces
{
    public interface IDiscMusicAppService : IDisposable
    {
        List<DiscMusicViewModel> GetAll(int pageIndex, int pageSize);
        DiscMusicViewModel GetById(Guid id);
    }
}
