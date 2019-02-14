using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeblueApi.Domain.Core.Bus;
using BeBlueApi.Application.Interfaces;
using BeBlueApi.Application.ViewModels;
using BeBlueApi.Domain.Commands;
using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Infra.Data.Repository.EventSourcing;
using System;
using System.Linq;
using System.Collections.Generic;


namespace BeBlueApi.Application.Services
{
    public class DiscMusicAppService : IDiscMusicAppService
    {
        private readonly IMapper _mapper;
        private readonly IDiscMusicRepository _repository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public DiscMusicAppService(IMapper mapper,
                                  IDiscMusicRepository repository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _repository = repository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public List<DiscMusicViewModel> GetAll(int pageIndex, int pageSize)
        {
            return _mapper.Map<List<DiscMusicViewModel>>(_repository.GetAll(pageIndex, pageSize));
        }

        public DiscMusicViewModel GetById(Guid id)
        {
            return _mapper.Map<DiscMusicViewModel>(_repository.GetById(id));
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
