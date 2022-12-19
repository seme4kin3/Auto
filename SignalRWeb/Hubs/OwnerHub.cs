using Microsoft.AspNetCore.SignalR;

namespace SignalRWeb.Hubs
{
    public class OwnerHub : Hub
    {
        public async Task NotifyWebUsers(string user, string message)
        {
            await Clients.All.SendAsync("DisplayNotification", user, message);
        }
    }

}
