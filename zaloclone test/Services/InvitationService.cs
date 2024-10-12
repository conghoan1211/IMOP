using Microsoft.EntityFrameworkCore;
using zaloclone_test.Models;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Services
{
    public interface IInvitationService
    {
        public Task<(string, List<Friend>)> GetAllInvitation(string UserId);
    }

    public class InvitationService : IInvitationService
    {
        private readonly Zalo_CloneContext _context;
        public InvitationService(Zalo_CloneContext context)
        {
            _context = context;
        }
        public async Task<(string, List<Friend>?)> GetAllInvitation(string UserId)
        {
           if(string.IsNullOrEmpty(UserId)) return ("UserId is null", null);
            var allInvitation = await _context.Friends.Where(x => x.Status == 0).ToListAsync();

            return(string.Empty, allInvitation);
        }
    }

    
}
