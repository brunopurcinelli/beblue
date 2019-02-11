using AutoMapper;
using BeBlueApi.Application.ViewModels;
using BeBlueApi.Domain.Commands;
using BeBlueApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cashback, CashbackViewModel>();
        }
    }
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CashbackViewModel, RegisterNewCashbackCommand>()
                .ConstructUsing(c => new RegisterNewCashbackCommand(c.MusicGender, c.WeekDay, c.Percent));
            CreateMap<CashbackViewModel, UpdateCashbackCommand>()
                .ConstructUsing(c => new UpdateCashbackCommand(c.Id, c.MusicGender, c.WeekDay, c.Percent));
        }
    }
}
