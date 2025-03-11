using AutoMapper;
using CusCake.Application.Annotations;
using CusCake.Application.ViewModels.AdminModels;
using CusCake.Application.ViewModels.AuthModels;
using CusCake.Application.ViewModels.AvailableCakeModels;
using CusCake.Application.ViewModels.BakeryModels;
using CusCake.Application.ViewModels.CakeDecorationModels;
using CusCake.Application.ViewModels.CakeExtraModels;
using CusCake.Application.ViewModels.CakeMessageModels;
using CusCake.Application.ViewModels.CakePartModels;
using CusCake.Application.ViewModels.CustomCakeModels;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Application.ViewModels.OrderModels;
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
            // CreateMap<AvailableCakeUpdateModel, AvailableCake>()
            //     .ConvertUsing<IgnoreNullValuesConverter<AvailableCakeUpdateModel, AvailableCake>>();

            #endregion

            // #region Cake Parts
            CreateMap<CakePartCreateModel, CakePartOption>().ReverseMap();
            CreateMap<CakePartUpdateModel, CakePartOption>().ReverseMap();
            // #endregion

            #region Cake Decorations
            CreateMap<CakeDecorationCreateModel, CakeDecorationOption>().ReverseMap();
            CreateMap<CakeDecorationUpdateModel, CakeDecorationOption>().ReverseMap();
            #endregion

            #region Cake Extras
            CreateMap<CakeExtraCreateModel, CakeExtraOption>().ReverseMap();
            CreateMap<CakeExtraUpdateModel, CakeExtraOption>().ReverseMap();
            #endregion

            #region Cake Messages
            CreateMap<CakeMessageOptionCreateModel, CakeMessageOption>().ReverseMap();
            CreateMap<CakeMessageOptionUpdateModel, CakeMessageOption>().ReverseMap();

            #endregion

            #region Cake Extras
            CreateMap<OrderCreateModel, Order>().ReverseMap();
            CreateMap<OrderDetailCreateModel, OrderDetail>().ReverseMap();
            #endregion

            #region CustomCake
            CreateMap<CustomCakeCreateModel, CustomCake>().ReverseMap();
            CreateMap<CakeMessageSelection, MessageSelection>().ReverseMap();
            // CreateMap<CakeMessageCreateDetail, CakeMessageDetail>().ReverseMap();
            #endregion

            #region Auth
            CreateMap<AuthCreateModel, Auth>().ReverseMap();
            CreateMap<AuthUpdateModel, Auth>().ReverseMap();
            CreateMap<AuthViewModel, Auth>().ReverseMap();
            #endregion


        }
    }
}
