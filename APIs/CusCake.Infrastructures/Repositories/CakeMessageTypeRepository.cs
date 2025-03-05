using CusCake.Application.Repositories;
using CusCake.Application.Services;
using CusCake.Application.Services.IServices;
using CusCake.Domain.Entities;

namespace CusCake.Infrastructures.Repositories
{
    public class CakeMessageTypeRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : GenericRepository<CakeMessageType>(context, currentTime, claimsService), ICakeMessageTypeRepository
    {
    }
    public class CakeMessageDetailRepository(AppDbContext context, ICurrentTime currentTime, IClaimsService claimsService) : GenericRepository<CakeMessageDetail>(context, currentTime, claimsService), ICakeMessageDetailRepository
    {
    }
}
