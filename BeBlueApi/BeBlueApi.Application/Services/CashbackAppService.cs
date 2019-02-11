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
using System.Text;

namespace BeBlueApi.Application.Services
{
    public class CashbackAppService : ICashbackAppService
    {
        private readonly IMapper _mapper;
        private readonly ICashbackRepository _CashbackRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public CashbackAppService(IMapper mapper,
                                  ICashbackRepository CashbackRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _CashbackRepository = CashbackRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<CashbackViewModel> GetAll()
        {
            return _CashbackRepository.GetAll().ProjectTo<CashbackViewModel>();
        }

        public CashbackViewModel GetById(Guid id)
        {
            return _mapper.Map<CashbackViewModel>(_CashbackRepository.GetById(id));
        }

        public void Register(CashbackViewModel CashbackViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCashbackCommand>(CashbackViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(CashbackViewModel CashbackViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCashbackCommand>(CashbackViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCashbackCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<CashbackHistoryData> GetAllHistory(Guid id)
        {
            return CashbackHistory.ToJavaScriptCashbackHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
