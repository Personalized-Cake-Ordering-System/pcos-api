using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    public class ReviewRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : GenericRepository<Review>(context, currentTime, claimsService), IReviewRepository
    {
    }
}
