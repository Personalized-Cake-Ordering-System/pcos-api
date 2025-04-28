using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.VoucherModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IVoucherService
{
    Task<Voucher> CreateAsync(VoucherCreateModel model);
    Task<Voucher> GetByIdAsync(Guid id);
    Task<(Pagination, List<Voucher>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Voucher, bool>>? filter = null);
    Task DeleteAsync(Guid id);
    Task<Voucher> UpdateAsync(Guid id, VoucherUpdateModel model);
    Task<Voucher?> GetVoucherByCodeAsync(string code, Guid bakeryId);
    Task<CustomerVoucher> AssignVoucherToCustomer(Guid id, AssignVoucherModel model);
    Task<(Pagination, List<CustomerVoucher>)> GetCustomerVouchersAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CustomerVoucher, bool>>? filter = null);
}


public class VoucherService(
    IUnitOfWork unitOfWork,
    IClaimsService claimsService,
    IMapper mapper
) : IVoucherService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClaimsService _claimsService = claimsService;

    public async Task<CustomerVoucher> AssignVoucherToCustomer(Guid id, AssignVoucherModel model)
    {
        var cus_voucher = new CustomerVoucher
        {
            VoucherId = id,
            CustomerId = model.CustomerId
        };

        await _unitOfWork.CustomerVoucherRepository.AddAsync(cus_voucher);
        await _unitOfWork.SaveChangesAsync();
        return cus_voucher;

    }

    public async Task<Voucher> CreateAsync(VoucherCreateModel model)
    {
        if (model.VoucherType == VoucherTypeConstants.SYSTEM && _claimsService.GetCurrentUserRole != RoleConstants.ADMIN)
            throw new BadRequestException("Cannot create system voucher");

        string code;
        do
        {
            code = GenerateVoucherCode.GenerateCode();
        }
        while (await GetVoucherByCodeAsync(code, _claimsService.GetCurrentUser) != null); // Dừng khi code chưa tồn tại

        var voucher = _mapper.Map<Voucher>(model);
        voucher.Code = code;
        voucher.BakeryId = model.VoucherType == VoucherTypeConstants.SYSTEM ? null : _claimsService.GetCurrentUser;

        await _unitOfWork.VoucherRepository.AddAsync(voucher);

        await _unitOfWork.SaveChangesAsync();

        return voucher;
    }



    public async Task DeleteAsync(Guid id)
    {
        var voucher = await GetByIdAsync(id);

        if (voucher.VoucherType == VoucherTypeConstants.SYSTEM && _claimsService.GetCurrentUserRole != RoleConstants.ADMIN)
            throw new BadRequestException("Cannot delete system voucher");

        _unitOfWork.VoucherRepository.SoftRemove(voucher);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<(Pagination, List<Voucher>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Voucher, bool>>? filter = null)
    {
        return await _unitOfWork.VoucherRepository.ToPagination(pageIndex, pageSize, filter: filter, includes: x => x.Bakery!);

    }

    public async Task<Voucher> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.VoucherRepository.GetByIdAsync(id, includes: x => x.Bakery!) ?? throw new BadRequestException("Voucher not found!");
    }

    public async Task<(Pagination, List<CustomerVoucher>)> GetCustomerVouchersAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CustomerVoucher, bool>>? filter = null)
    {
        var includes = QueryHelper.Includes<CustomerVoucher>(
            x => x.Customer!,
            x => x.Voucher!);
        return await _unitOfWork.CustomerVoucherRepository.ToPagination(pageIndex, pageSize, filter: filter, includes: includes);

    }

    public async Task<Voucher?> GetVoucherByCodeAsync(string code, Guid bakeryId)
    {
        return await _unitOfWork.VoucherRepository.FirstOrDefaultAsync(x => x.Code == code && x.BakeryId == bakeryId);
    }

    public async Task<Voucher> UpdateAsync(Guid id, VoucherUpdateModel model)
    {
        var voucher = await GetByIdAsync(id);

        if (voucher.VoucherType == VoucherTypeConstants.SYSTEM && _claimsService.GetCurrentUserRole != RoleConstants.ADMIN)
            throw new BadRequestException("Cannot update system voucher");

        if (voucher.VoucherType != VoucherTypeConstants.SYSTEM && voucher.BakeryId != _claimsService.GetCurrentUser)
            throw new BadRequestException("No permission");

        _mapper.Map(model, voucher);

        _unitOfWork.VoucherRepository.Update(voucher);
        await _unitOfWork.SaveChangesAsync();
        return voucher;
    }
}
