using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeblueApi.Domain.Core.Bus;
using BeBlueApi.Application.EventSourcedNormalizers;
using BeBlueApi.Application.Interfaces;
using BeBlueApi.Application.ViewModels;
using BeBlueApi.Domain.Commands;
using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Infra.Data.Repository.EventSourcing;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Application.Services
{
    public class MusicGenderAppService : IMusicGenderAppService
    {
        private readonly IMapper _mapper;
        private readonly IMusicGenderRepository _repository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public MusicGenderAppService(IMapper mapper,
                                  IMusicGenderRepository repository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _repository = repository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<MusicGenderViewModel> GetAll()
        {
            return _repository.GetAll().ProjectTo<MusicGenderViewModel>();
        }

        public MusicGenderViewModel GetById(Guid id)
        {
            return _mapper.Map<MusicGenderViewModel>(_repository.GetById(id));
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
