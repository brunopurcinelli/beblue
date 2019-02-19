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
    public class SalesLineAppService : ISalesLineAppService
    {
        private readonly IMapper _mapper;
        private readonly ISalesLineRepository _repository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public SalesLineAppService(IMapper mapper,
                                  ISalesLineRepository repository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _repository = repository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<SalesLineViewModel> GetAll()
        {
            return _repository.GetAll().ProjectTo<SalesLineViewModel>();
        }

        public SalesLineViewModel GetById(Guid id)
        {
            return _mapper.Map<SalesLineViewModel>(_repository.GetById(id));
        }

        public void Register(SalesLineViewModel SalesLineViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewSalesLineCommand>(SalesLineViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(SalesLineViewModel SalesLineViewModel)
        {
            var updateCommand = _mapper.Map<UpdateSalesLineCommand>(SalesLineViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveSalesLineCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<SalesLineViewModel> GetBySalesId(Guid id)
        {
            return _mapper.Map<List<SalesLineViewModel>>(_repository.GetBySalesId(id));
        }
    }
}
