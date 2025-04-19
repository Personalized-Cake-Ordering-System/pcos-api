
using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.ViewModels.ReviewModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Hangfire;
using UnauthorizedAccessException = CusCake.Application.GlobalExceptionHandling.Exceptions.UnauthorizedAccessException;

namespace CusCake.Application.Services;

public interface IReviewService
{
    Task<Review> CreateAsync(ReviewCreateModel model);
    Task<Review> UpdateAsync(Guid id, ReviewUpdateModel model);
    Task DeleteAsync(Guid id);
    Task<Review> GetByIdAsync(Guid id);
}

public class ReviewService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IClaimsService claimsService,
    IBackgroundJobClient backgroundJobClient,
    IBakeryMetricService bakeryMetricService,
    IAvailableCakeMetricService availableCakeMetricService
) : IReviewService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IClaimsService _claimsService = claimsService;
    private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
    private readonly IBakeryMetricService _bakeryMetricService = bakeryMetricService;
    private readonly IAvailableCakeMetricService _availableCakeMetricService = availableCakeMetricService;
    public async Task<Review> CreateAsync(ReviewCreateModel model)
    {
        var cake_review = _mapper.Map<Review>(model);

        cake_review.CustomerId = _claimsService.GetCurrentUser;

        if (model.ReviewType == ReviewTypeConstants.AVAILABLE_CAKE_REVIEW)
        {
            var order_detail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(model.OrderDetailId!.Value)
                      ?? throw new BadRequestException("Order not found!");
            if (order_detail.CakeReviewId != null) throw new BadRequestException("Only one review for it order_detail!");
            order_detail.CakeReviewId = cake_review.Id;
            _unitOfWork.OrderDetailRepository.Update(order_detail);
            await _unitOfWork.ReviewRepository.AddAsync(cake_review);
            await _unitOfWork.SaveChangesAsync();
            _backgroundJobClient.Enqueue(() => _availableCakeMetricService.CalculateAvailableCakeMetricsAsync(order_detail.AvailableCakeId!.Value));
            return cake_review;
        }


        await _unitOfWork.ReviewRepository.AddAsync(cake_review);
        await _unitOfWork.SaveChangesAsync();

        _backgroundJobClient.Enqueue(() => _bakeryMetricService.ReCalculateBakeryMetricsAsync(cake_review.BakeryId));


        return cake_review;
    }

    public async Task DeleteAsync(Guid id)
    {
        var review = await GetByIdAsync(id);
        if (_claimsService.GetCurrentUser != review.CustomerId) throw new UnauthorizedAccessException("No permission to edit");

        _unitOfWork.ReviewRepository.SoftRemove(review);
        await _unitOfWork.SaveChangesAsync();
        if (review.ReviewType == ReviewTypeConstants.BAKERY_REVIEW)
        {
            _backgroundJobClient.Enqueue(() => _bakeryMetricService.ReCalculateBakeryMetricsAsync(review.BakeryId));
        }
        else if (review.ReviewType == ReviewTypeConstants.AVAILABLE_CAKE_REVIEW)
        {
            _backgroundJobClient.Enqueue(() => _availableCakeMetricService.CalculateAvailableCakeMetricsAsync(review.AvailableCakeId!.Value));
        }
    }

    public async Task<Review> GetByIdAsync(Guid id)
    {
        var includes = QueryHelper.Includes<Review>(
            x => x.Customer!,
            x => x.Bakery!,
            x => x.Image!);
        return await _unitOfWork.ReviewRepository.GetByIdAsync(id) ??
            throw new BadRequestException("Not found!");
    }


    public async Task<Review> UpdateAsync(Guid id, ReviewUpdateModel model)
    {
        var review = await GetByIdAsync(id);
        if (_claimsService.GetCurrentUser != review.CustomerId) throw new UnauthorizedAccessException("No permission to edit");

        review.Content = model.Content;
        review.Rating = model.Rating;
        review.ImageId = model.ImageId;

        _unitOfWork.ReviewRepository.Update(review);
        await _unitOfWork.SaveChangesAsync();

        if (review.ReviewType == ReviewTypeConstants.BAKERY_REVIEW)
        {
            _backgroundJobClient.Enqueue(() => _bakeryMetricService.ReCalculateBakeryMetricsAsync(review.BakeryId));
        }
        else if (review.ReviewType == ReviewTypeConstants.AVAILABLE_CAKE_REVIEW)
        {
            _backgroundJobClient.Enqueue(() => _availableCakeMetricService.CalculateAvailableCakeMetricsAsync(review.AvailableCakeId!.Value));
        }
        return review;
    }
}
