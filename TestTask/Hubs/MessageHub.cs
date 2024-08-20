using Microsoft.AspNetCore.SignalR;
using TestTask.API.DTO;

namespace TestTask.API.Hubs;

public class MessageHub:Hub
{
    public Task Send(MessageDto message)
    {
        return Clients.All.SendAsync("Recieve", message);
    }
}
