using BeBlueApi.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Application.Interfaces
{
    public interface IMusicGenderAppService : IDisposable
    {
        IEnumerable<MusicGenderViewModel> GetAll();
        MusicGenderViewModel GetById(Guid id);
    }
}
