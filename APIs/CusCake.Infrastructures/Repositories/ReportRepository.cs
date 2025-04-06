using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    public class ReportRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : GenericRepository<Report>(context, currentTime, claimsService), IReportRepository
    {
    }
}
