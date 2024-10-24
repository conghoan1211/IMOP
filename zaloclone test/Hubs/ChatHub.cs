using Microsoft.AspNetCore.SignalR;

namespace zaloclone_test.MyHub
{
    public class ChatHub : Hub
    {
        public async Task SenddMessage(string sender, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", sender, message);
        }
    }
}
