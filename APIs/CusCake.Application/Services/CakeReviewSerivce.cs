
using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.ViewModels.CakeReviewModels;
using CusCake.Domain.Entities;
using UnauthorizedAccessException = CusCake.Application.GlobalExceptionHandling.Exceptions.UnauthorizedAccessException;

namespace CusCake.Application.Services;

public interface ICakeReviewService
{
    Task<CakeReview> CreateAsync(CakeReviewCreateModel model);
    Task<CakeReview> UpdateAsync(Guid id, CakeReviewUpdateModel model);
    Task DeleteAsync(Guid id);
    Task<CakeReview> GetByIdAsync(Guid id);
}

public class CakeReviewService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IClaimsService claimsService
) : ICakeReviewService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IClaimsService _claimsService = claimsService;

    public async Task<CakeReview> CreateAsync(CakeReviewCreateModel model)
    {
        var order_detail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(model.OrderDetailId)
            ?? throw new BadRequestException("Order not found!");
        if (order_detail.CakeReviewId != null) throw new BadRequestException("Only one review for it order_detail!");
        var cake_review = _mapper.Map<CakeReview>(model);
        cake_review.CustomerId = _claimsService.GetCurrentUser;
        order_detail.CakeReviewId = cake_review.Id;
        await _unitOfWork.CakeReviewRepository.AddAsync(cake_review);
        _unitOfWork.OrderDetailRepository.Update(order_detail);
        await _unitOfWork.SaveChangesAsync();
        return cake_review;
    }

    public async Task DeleteAsync(Guid id)
    {
        var cake_review = await GetByIdAsync(id);
        if (_claimsService.GetCurrentUser != cake_review.CustomerId) throw new UnauthorizedAccessException("No permission to edit");

        _unitOfWork.CakeReviewRepository.SoftRemove(cake_review);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<CakeReview> GetByIdAsync(Guid id)
    {
        var includes = QueryHelper.Includes<CakeReview>(
            x => x.Customer!,
            x => x.Bakery!,
            x => x.Image!);
        return await _unitOfWork.CakeReviewRepository.GetByIdAsync(id) ??
            throw new BadRequestException("Not found!");
    }


    public async Task<CakeReview> UpdateAsync(Guid id, CakeReviewUpdateModel model)
    {
        var cake_review = await GetByIdAsync(id);
        if (_claimsService.GetCurrentUser != cake_review.CustomerId) throw new UnauthorizedAccessException("No permission to edit");
        _mapper.Map(model, cake_review);
        _unitOfWork.CakeReviewRepository.Update(cake_review);
        await _unitOfWork.SaveChangesAsync();
        return cake_review;
    }
}
