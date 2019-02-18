using AutoMapper;
using BeBlueApi.Application.ViewModels;
using BeBlueApi.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeBlueApi.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<DiscMusicViewModel, RegisterNewDiscMusicCommand>().ConstructUsing(c => new RegisterNewDiscMusicCommand(c.Name, c.IdGender, c.Price));
            CreateMap<DiscMusicViewModel, UpdateDiscMusicCommand>().ConstructUsing(c => new UpdateDiscMusicCommand(c.Id, c.IdGender, c.Name, c.Price));

            CreateMap<SalesViewModel, RegisterNewSalesCommand>().ConstructUsing(c => new RegisterNewSalesCommand(c.SalesLines.ToList().ConvertAll(x => 
                                                                                                                                                    new LineCommand(
                                                                                                                                                                x.IdDisc,
                                                                                                                                                                x.Quantity,
                                                                                                                                                                x.PriceUnit,
                                                                                                                                                                x.SalesPrice,
                                                                                                                                                                x.Cashback))));

            CreateMap<SalesLineViewModel, RegisterNewSalesLineCommand>().ConstructUsing(c => new RegisterNewSalesLineCommand(c.IdSales, c.IdDisc, c.Quantity, c.PriceUnit, c.SalesPrice, c.Cashback));
            CreateMap<SalesLineViewModel, UpdateSalesLineCommand>().ConstructUsing(c => new UpdateSalesLineCommand(c.Id, c.IdSales, c.IdDisc, c.Quantity, c.PriceUnit, c.Cashback));
        }
    }
}







