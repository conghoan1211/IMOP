using Microsoft.EntityFrameworkCore;
using zaloclone_test.Models;

namespace zaloclone_test.Services
{
    public interface IMessageService
    {
        public Task AddMessage(string senderId, string conversationId, string messageBlockId, string dateBlockId, string content);
    }
    public class MessageService : IMessageService
    {
        private readonly Zalo_CloneContext _context;
        public MessageService(Zalo_CloneContext context)
        {
            _context = context;
        }

        public async Task AddMessage(string senderId, string conversationId, string messageBlockId, string dateBlockId, string content)
        {
            DateTime now = DateTime.Now;
            Message message = new Message();
            message.MessageId = Guid.NewGuid().ToString();
            message.SenderId = senderId;
            MessageBlock messageBlock = await _context.MessageBlocks
                .Where(mb => now.Subtract(mb.FirstSendDate).TotalMinutes < 15)
                .FirstAsync();
            if(messageBlock == null)
            {
                messageBlock = new MessageBlock();
                messageBlock.MessageBlockId = Guid.NewGuid().ToString();
                messageBlock.FirstSendDate = now;
                messageBlock.ConversationId = conversationId;
                _context.MessageBlocks.Add(messageBlock);
            }
            _context.Messages.Add(message);
        }
    }
}
