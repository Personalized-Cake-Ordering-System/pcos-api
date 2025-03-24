using AutoMapper;
using CusCake.Application.Services.IServices;
using CusCake.Application.ViewModels.CartModels;

namespace CusCake.Application.Services;

public interface ICartService
{
    Task<CartEntity> GetCartAsync();
    Task<List<CartEntity>> GetAllCartAsync();
    Task<CartEntity> UpsertAsync(CartActionModel cart);
    Task DeleteAsync();
    // Task InsertAsync(CartEntity cart);
}


public class CartService(
    IUnitOfWork unitOfWork,
    IClaimsService claimsService,
    IMapper mapper) : ICartService
{
    private const string TABLE = "carts";
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClaimsService _claimsService = claimsService;
    private readonly IMapper _mapper = mapper;
    public async Task<CartEntity> GetCartAsync()
    {
        return await _unitOfWork.MongoRepository.GetByIdAsync<CartEntity>(TABLE, _claimsService.GetCurrentUser);
    }

    public async Task<CartEntity> UpsertAsync(CartActionModel model)
    {
        var cart = await GetCartAsync();
        if (cart == null)
        {
            return await InsertAsync(model);
        }
        _mapper.Map(model, cart);
        await _unitOfWork.MongoRepository.UpsertAsync<CartEntity>(TABLE, _claimsService.GetCurrentUser, cart);
        return cart;
    }

    public async Task DeleteAsync()
    {
        await _unitOfWork.MongoRepository.DeleteAsync<CartEntity>(TABLE, _claimsService.GetCurrentUser);
    }

    public async Task<CartEntity> InsertAsync(CartActionModel model)
    {
        var cart = _mapper.Map<CartEntity>(model);
        cart.CustomerId = _claimsService.GetCurrentUser;
        await _unitOfWork.MongoRepository.InsertAsync<CartEntity>(TABLE, cart);
        return cart;
    }

    public async Task<List<CartEntity>> GetAllCartAsync()
    {
        return await _unitOfWork.MongoRepository.GetAllAsync<CartEntity>(TABLE);
    }
}
