using AutoMapper;
using CusCake.Application.ViewModels.AdminModels;
using CusCake.Application.ViewModels.BakeryModel;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            #region Customer
            CreateMap<Admin, AdminCreateModel>().ReverseMap();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<CustomerCreateModel, Customer>().ReverseMap();
            CreateMap<CustomerUpdateModel, Customer>().ReverseMap();
            #endregion

            #region Bakery
            CreateMap<BakeryCreateModel, Bakery>().ReverseMap();
            CreateMap<BakeryUpdateModel, Bakery>().ReverseMap();
            #endregion
        }
    }
}
