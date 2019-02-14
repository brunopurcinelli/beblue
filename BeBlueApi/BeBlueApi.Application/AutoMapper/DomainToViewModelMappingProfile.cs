using AutoMapper;
using BeBlueApi.Application.ViewModels;
using BeBlueApi.Domain.Models;

namespace BeBlueApi.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cashback, CashbackViewModel>();
            CreateMap<DiscMusic, DiscMusicViewModel>();
            CreateMap<MusicGender, MusicGenderViewModel>();
            CreateMap<Sales, SalesViewModel>();
            CreateMap<SalesLine, SalesLineViewModel>();
        }
    }
}







