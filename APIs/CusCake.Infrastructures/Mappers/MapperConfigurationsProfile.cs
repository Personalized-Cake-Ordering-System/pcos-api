using AutoMapper;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<CustomerCreateModel, Customer>().ReverseMap();
        }
    }
}
