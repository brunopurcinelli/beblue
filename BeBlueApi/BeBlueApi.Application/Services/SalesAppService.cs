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
    public class SalesAppService : ISalesAppService
    {
        private readonly IMapper _mapper;
        private readonly ISalesRepository _repository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public SalesAppService(IMapper mapper,
                                  ISalesRepository repository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _repository = repository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public List<SalesViewModel> GetAll(int pageIndex, int pageSize, DateTime dateInitial, DateTime dateFinal)
        {
            return _mapper.Map<List<SalesViewModel>>(_repository.GetAll(pageIndex, pageSize, dateInitial, dateFinal));
        }

        public SalesViewModel GetById(Guid id)
        {
            return _mapper.Map<SalesViewModel>(_repository.GetById(id));
        }

        public void Register(SalesViewModel salesViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewSalesCommand>(salesViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(SalesViewModel SalesViewModel)
        {
            var updateCommand = _mapper.Map<UpdateSalesCommand>(SalesViewModel);
            Bus.SendCommand(updateCommand);
        }
        public void Remove(Guid id)
        {
            var removeCommand = new RemoveSalesCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
