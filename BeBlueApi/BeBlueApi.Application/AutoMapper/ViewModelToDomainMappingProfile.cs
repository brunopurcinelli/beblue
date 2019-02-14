using AutoMapper;
using BeBlueApi.Application.ViewModels;
using BeBlueApi.Domain.Commands;
using System.Collections.Generic;
using System.Linq;

namespace BeBlueApi.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CashbackViewModel, RegisterNewCashbackCommand>().ConstructUsing(c => new RegisterNewCashbackCommand(c.IdGender, c.WeekDay, c.Percent));
            CreateMap<CashbackViewModel, UpdateCashbackCommand>().ConstructUsing(c => new UpdateCashbackCommand(c.Id, c.IdGender, c.WeekDay, c.Percent));

            CreateMap<DiscMusicViewModel,   RegisterNewDiscMusicCommand>().ConstructUsing   (c => new RegisterNewDiscMusicCommand(c.Name, c.IdGender, c.Price));
            CreateMap<DiscMusicViewModel,   UpdateDiscMusicCommand>().ConstructUsing    (c => new UpdateDiscMusicCommand(c.Id, c.IdGender, c.Name, c.Price));

            CreateMap<MusicGenderViewModel, RegisterNewMusicGenderCommand>().ConstructUsing (c => new RegisterNewMusicGenderCommand(c.Description));
            CreateMap<MusicGenderViewModel, UpdateMusicGenderCommand>().ConstructUsing  (c => new UpdateMusicGenderCommand(c.Id, c.Description));

            var lines = new List<SalesLineCommand>();            

            CreateMap<SalesRequest, RegisterNewSalesCommand>().ConstructUsing (c => new RegisterNewSalesCommand(new List<SalesLineCommand>() {}));
            CreateMap<SalesViewModel, UpdateSalesCommand>().ConstructUsing (c => new UpdateSalesCommand(c.Id, c.SalesDate, c.TotalAmount, c.TotalCashback));

            CreateMap<SalesLineViewModel,   RegisterNewSalesLineCommand>().ConstructUsing   (c => new RegisterNewSalesLineCommand(c.IdSales, c.IdItem, c.DiscName, c.Quantity, c.PriceUnit, c.Cashback));
            CreateMap<SalesLineViewModel,   UpdateSalesLineCommand>().ConstructUsing    (c => new UpdateSalesLineCommand(c.Id, c.IdSales, c.IdItem, c.DiscName, c.Quantity, c.PriceUnit, c.Cashback));
        }
    }
}







