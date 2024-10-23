using Microsoft.EntityFrameworkCore;
using zaloclone_test.Models;

namespace zaloclone_test.Services
{
    public interface IMessageService
    {
        public Task<List<Message>> GetLastMessages(string ownerId);
    }
    public class MessageService : IMessageService
    {
        private readonly Zalo_CloneContext _context;
        public MessageService(Zalo_CloneContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetLastMessages(string ownerId)
        {
            throw new NotImplementedException();
        }
    }
}
