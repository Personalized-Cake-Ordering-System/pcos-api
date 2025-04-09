using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.ReportModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Newtonsoft.Json;

namespace CusCake.Application.Services;

public interface IReportService
{
    Task<Report> CreateAsync(ReportCreateModel model);
    Task<(Pagination, List<Report>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Report, bool>>? filter = null);
    Task<Report> GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<Report> UpdateAsync(Guid id, ReportUpdateModel model);
    Task<bool> ApproveAsync(Guid id, bool isApproved = true);

}

public class ReportService(
    IUnitOfWork unitOfWork,
    IClaimsService claimsService,
    IMapper mapper,
    IBakeryService bakeryService,
    INotificationService notificationService,
    IAuthService authService
) : IReportService
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClaimsService _claimsService = claimsService;
    private readonly IMapper _mapper = mapper;
    private readonly IBakeryService _bakeryService = bakeryService;
    private readonly IAuthService _authService = authService;
    private readonly INotificationService _notificationService = notificationService;
    public async Task<bool> ApproveAsync(Guid id, bool isApproved = true)
    {
        var report = await GetByIdAsync(id);
        if (report.Status != ReportStatusConstants.PENDING)
            throw new BadRequestException("Report has been checked!");
        if (isApproved)
        {
            await HandleApproveAsync(report);
        }
        report.Status = isApproved ? ReportStatusConstants.ACCEPTED : ReportStatusConstants.REJECTED;
        _unitOfWork.ReportRepository.Update(report);
        return await _unitOfWork.SaveChangesAsync();
    }


    private async Task<CustomerVoucher> AssignVoucherToCustomer(double discountPercentage, Guid customer_id)
    {
        var voucher = await _unitOfWork.VoucherRepository.FirstOrDefaultAsync(x => x.VoucherType == VoucherTypeConstants.SYSTEM && x.DiscountPercentage == discountPercentage);

        var cus_voucher = new CustomerVoucher
        {
            VoucherId = voucher!.Id,
            CustomerId = customer_id
        };

        await _unitOfWork.CustomerVoucherRepository.AddAsync(cus_voucher);
        return cus_voucher;

    }
    private async Task HandleApproveAsync(Report report)
    {
        var approved_reports = await _unitOfWork.ReportRepository.WhereAsync(x => x.BakeryId == report.BakeryId && x.Status == ReportStatusConstants.ACCEPTED);
        if (approved_reports.Count == 0)
        {
            await AssignVoucherToCustomer(0.1, report.CustomerId);
            return;
        }
        if (approved_reports.Count == 1)
        {
            await AssignVoucherToCustomer(0.4, report.CustomerId);
            return;
        }
        await AssignVoucherToCustomer(0.7, report.CustomerId);

        var bakery = await _bakeryService.GetByIdAsync(report.BakeryId);
        bakery.Status = BakeryStatusConstants.BANNED;
        bakery.BannedAt = DateTime.Now;

        _unitOfWork.BakeryRepository.Update(bakery);

    }

    public async Task<Report> CreateAsync(ReportCreateModel model)
    {
        var report = _mapper.Map<Report>(model);
        var admin = await _authService.GetAdminAsync();
        var order = model.OrderId != null ? await _unitOfWork.OrderRepository.GetByIdAsync(model.OrderId!.Value) : null;

        if (order != null && order.CustomerId != _claimsService.GetCurrentUser)
            throw new BadRequestException("No permission to access order!");

        report.CustomerId = _claimsService.GetCurrentUser;
        report.Type = model.OrderId != null ? ReportTypeConstants.ORDER_REPORT : ReportTypeConstants.BAKERY_REPORT;
        report.ReportFiles = await _unitOfWork.StorageRepository.WhereAsync(s => model.ReportFileIds.Contains(s.Id));

        await _unitOfWork.ReportRepository.AddAsync(report);
        await _unitOfWork.SaveChangesAsync();

        var reportJson = JsonConvert.SerializeObject(report);

        await _notificationService.CreateAdminNotificationAsync(report.Id, NotificationType.NEW_REPORT, admin.EntityId);
        await _notificationService.SendNotificationAsync(admin.EntityId, reportJson, NotificationType.NEW_REPORT);

        return report;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var report = await GetByIdAsync(id);
        if (report.Status != ReportStatusConstants.PENDING)
            throw new BadRequestException("Only delete when status is PENDING");
        _unitOfWork.ReportRepository.SoftRemove(report);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<(Pagination, List<Report>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Report, bool>>? filter = null)
    {
        var includes = QueryHelper.Includes<Report>(x => x.Customer!, x => x.Bakery!, x => x.Order!);

        return await _unitOfWork.ReportRepository.ToPagination(pageIndex, pageSize, filter: filter, includes: includes);
    }

    public async Task<Report> GetByIdAsync(Guid id)
    {
        var includes = QueryHelper.Includes<Report>(x => x.Customer!, x => x.Bakery!, x => x.Order!);

        return await _unitOfWork.ReportRepository.GetByIdAsync(id, includes: includes) ?? throw new BadRequestException("Id is not exist!");
    }

    public async Task<Report> UpdateAsync(Guid id, ReportUpdateModel model)
    {
        var report = await GetByIdAsync(id);
        if (report.Status != ReportStatusConstants.PENDING)
            throw new BadRequestException("Only update when status is PENDING");
        _mapper.Map(model, report);
        report.ReportFiles = await _unitOfWork.StorageRepository.WhereAsync(s => model.ReportFileIds.Contains(s.Id));

        _unitOfWork.ReportRepository.Update(report);
        await _unitOfWork.SaveChangesAsync();
        return report;
    }
}
