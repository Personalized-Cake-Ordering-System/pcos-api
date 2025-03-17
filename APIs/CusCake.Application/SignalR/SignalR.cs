using Microsoft.AspNetCore.SignalR;

namespace CusCake.Application.SignalR;

public class SignalRConnection : Hub
{
    public async Task SendNotification(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}