using AutoMapper;
using CusCake.Application.ViewModels.AdminModels;
using CusCake.Application.ViewModels.AvailableCakeModels;
using CusCake.Application.ViewModels.BakeryModels;
using CusCake.Application.ViewModels.CakePartModels;
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

            #region Available Cakes
            CreateMap<AvailableCakeCreateModel, AvailableCake>().ReverseMap();
            CreateMap<AvailableCakeUpdateModel, AvailableCake>().ReverseMap();
            #endregion

            #region Cake Parts
            CreateMap<CakePartCreateModel, CakePart>().ReverseMap();
            CreateMap<CakePartUpdateModel, CakePart>().ReverseMap();
            #endregion
        }
    }
}
