using zaloclone_test.Models;

namespace zaloclone_test.Services
{
    public interface IConversationService
    {
        public Task<List<Conversation>> GetOwnedConversations(string userId);
        public Task<Conversation> GetConversationById();
        public Task<Conversation> AddConversation();
    }
    public class ConversationService
    {
    }
}
