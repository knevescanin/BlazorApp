using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class UserHub : Hub
{
    public async Task UpdateUser(string userId)
    {
        // Call a method on the client side to update the UI
        await Clients.All.SendAsync("UserUpdated", userId);
    }
}
