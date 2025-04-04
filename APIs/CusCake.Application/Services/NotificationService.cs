using System.Linq.Expressions;
using CusCake.Application.Extensions;
using CusCake.Application.SignalR;
using CusCake.Application.Utils;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CusCake.Application.Services;

public interface INotificationService
{
    // Task<Notification> SendAdminNotificationAsync(Notification notification);
    Task SendNotificationAsync(Guid connectionId, string message, string type);
    Task<Notification> CreateOrderNotificationAsync(Guid targetEntityId, string type, Guid? bakeryId, Guid? customerId);
    Task<(Pagination, List<Notification>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Notification, bool>>? filter = null);

}

public class NotificationService(
    IHubContext<SignalRConnection> hubContext,
    IUnitOfWork unitOfWork
) : INotificationService
{
    private readonly IHubContext<SignalRConnection> _hubContext = hubContext;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task SendNotificationAsync(Guid connectionId, string message, string type)
    {
        var notification = new
        {
            Type = type,
            Message = message
        };

        var json = JsonConvert.SerializeObject(notification);

        await _hubContext.Clients
            .All
            .SendAsync("messageReceived", connectionId.ToString(), json);


    }

    public async Task<Notification> CreateOrderNotificationAsync(Guid targetEntityId, string type, Guid? bakeryId, Guid? customerId)
    {
        var notification = new Notification
        {
            Title = NotificationType.GetTitleByType(type),
            Content = NotificationType.GetContentByType(type),
            Type = type,
            TargetEntityId = targetEntityId,
            BakeryId = bakeryId,
            CustomerId = customerId
        };

        await _unitOfWork.NotificationRepository.AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();
        return notification;
    }

    public async Task<(Pagination, List<Notification>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Notification, bool>>? filter = null)
    {
        var includes = QueryHelper.Includes<Notification>(x => x.Customer!, x => x.Bakery!);
        return await _unitOfWork.NotificationRepository.ToPagination(pageIndex, pageSize, filter: filter, includes: includes);
    }
}

