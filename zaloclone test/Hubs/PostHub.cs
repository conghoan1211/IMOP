using Microsoft.AspNetCore.SignalR;

namespace zaloclone_test.MyHub
{
    public class PostHub : Hub
    {
        public async Task SendLikeUpdate(string postId, int likesCount)
        {
            await Clients.All.SendAsync("ReceiveLikeUpdate", postId, likesCount);
        }
    }
}
