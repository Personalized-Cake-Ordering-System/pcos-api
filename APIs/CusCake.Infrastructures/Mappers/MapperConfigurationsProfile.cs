using AutoMapper;
using CusCake.Application.ViewModels.AdminModels;
using CusCake.Application.ViewModels.AuthModels;
using CusCake.Application.ViewModels.AvailableCakeModels;
using CusCake.Application.ViewModels.BakeryModels;
using CusCake.Application.ViewModels.CakeDecorationModels;
using CusCake.Application.ViewModels.CakeExtraModels;
using CusCake.Application.ViewModels.CakeMessageModels;
using CusCake.Application.ViewModels.CakePartModels;
using CusCake.Application.ViewModels.CakeReviewModels;
using CusCake.Application.ViewModels.CartModels;
using CusCake.Application.ViewModels.CustomCakeModels;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Application.ViewModels.OrderModels;
using CusCake.Application.ViewModels.ReportModels;
using CusCake.Application.ViewModels.TransactionModels;
using CusCake.Application.ViewModels.VoucherModels;
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

            #region Orders
            CreateMap<OrderCreateModel, Order>().ReverseMap();
            CreateMap<OrderUpdateModel, Order>()
                // .ForMember(x => x.ShippingType, option => option.Ignore())
                // .ForMember(x => x.ShippingAddress, option => option.Ignore())
                // .ForMember(x => x.VoucherCode, option => option.Ignore())
                .ReverseMap();
            CreateMap<OrderDetailCreateModel, OrderDetail>().ReverseMap();

            CreateMap<TransactionWebhookModel, Transaction>()
                .ForMember(x => x.Id, option => option.Ignore())
                .ForMember(x => x.TransactionId, option => option.MapFrom(x => x.Id))
                .ReverseMap();

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

            #region  Voucher
            CreateMap<VoucherCreateModel, Voucher>().ReverseMap();
            CreateMap<VoucherUpdateModel, Voucher>().ReverseMap();
            #endregion

            #region Cart
            CreateMap<CartActionModel, CartEntity>().ReverseMap();
            #endregion

            #region Cart
            CreateMap<CakeReview, CakeReviewCreateModel>().ReverseMap();
            CreateMap<CakeReview, CakeReviewUpdateModel>().ReverseMap();
            #endregion

            #region Report
            CreateMap<Report, ReportCreateModel>().ReverseMap();
            CreateMap<Report, ReportUpdateModel>()
                .ForMember(x => x.OrderId, option => option.Ignore())
                .ReverseMap();
            #endregion
        }
    }
}
