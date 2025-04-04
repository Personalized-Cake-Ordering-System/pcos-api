using Microsoft.AspNetCore.SignalR;

namespace CusCake.Application.SignalR;

public class SignalRConnection : Hub
{
    private static readonly Dictionary<string, string> userConnections = [];

    // Method to register user and map to connectionId
    public async Task RegisterUser(string userId)
    {
        var connectionId = Context.ConnectionId;
        if (!userConnections.ContainsKey(userId))
        {
            userConnections[userId] = connectionId;
        }
        else
        {
            // If the user reconnects, update the connectionId
            userConnections[userId] = connectionId;
        }
        Console.WriteLine(connectionId);
        await Clients.Client(connectionId).SendAsync("ReceiveMessage", "User registered with ID: " + userId);
    }

    public async Task<bool> SendNotification(string userId, string message)
    {
        try
        {
            await Clients.All.SendAsync("messageReceived", userId, message);
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task SendMessageAsync(string user, string message)
    {
        await Clients.All.SendAsync("messageReceived", user, message);
    }
}