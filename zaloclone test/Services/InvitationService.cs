using Microsoft.EntityFrameworkCore;
using zaloclone_test.Helper;
using zaloclone_test.Models;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Services
{
    public interface IInvitationService
    {
        public Task<(string, List<InvitationVM>?)> GetAllInvitation(string UserId);
        public Task<(string, List<InvitationVM>?)> GetAllRequested(string UserId);
        public Task<string> RevokeInvitation(string UserId1, string UserId2);
        public Task<string> AcceptInvitation(string UserId1, string UserId2);
        public Task<string> SendRequest(string UserId1, string UserOtherId);

    }

    public class InvitationService : IInvitationService
    {
        private readonly Zalo_CloneContext _context;
        public InvitationService(Zalo_CloneContext context)
        {
            _context = context;
        }
        public async Task<(string, List<InvitationVM>?)> GetAllInvitation(string UserId)
        {
            if (string.IsNullOrEmpty(UserId)) return ("UserId is null", null);

            var allInvitation = await _context.Friends
                .Where(f => f.UpdateUser == UserId && f.Status == 0) 
                .Join(_context.Users, f => f.CreateUser, u => u.UserId, (f, u) => new InvitationVM // Chọn các trường cần thiết
                {
                    UserID = u.UserId,
                    UserName = u.Username, 
                    Avatar = u.Avatar,  
                    CreateAt = f.CreateAt,  
                    Status = f.Status  
                })
                .ToListAsync();

            return (string.Empty, allInvitation);
        }


        public async Task<(string, List<InvitationVM>?)> GetAllRequested(string UserId)
        {
            if (string.IsNullOrEmpty(UserId)) return ("UserId is null", null);

            var sentInvitation = await _context.Friends
                .Where(f => f.UserId1 == UserId && f.CreateUser == UserId && f.Status == 0)
                .Join(_context.Users, f => f.UserId2, u => u.UserId, (f, u) => new InvitationVM
                {
                    UserID = u.UserId,
                    UserName = u.Username, 
                    Avatar = u.Avatar, 
                    CreateAt = f.CreateAt ?? DateTime.Now,  
                    Status = f.Status 
                })
                .ToListAsync();

            return (string.Empty, sentInvitation);
        }

        public async Task<string> RevokeInvitation(string UserId1, string UserId2)
        {
            if (string.IsNullOrEmpty(UserId1)) return "UserId is null";
            var friend = await _context.Friends.FirstOrDefaultAsync(f => (f.UserId1 == UserId1 && f.UserId2 == UserId2) || (f.UserId1 == UserId2 && f.UserId2 == UserId1) && f.Status == (int)FriendStatus.Pending);
            if (friend == null) return "Friend not found";
            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();
            return "";
        }

        public async Task<string> AcceptInvitation(string UserId1, string UserId2)
        {
            if (string.IsNullOrEmpty(UserId1)) return "UserId is null";
            var friend = await _context.Friends.FirstOrDefaultAsync(f => f.UserId2 == UserId1 && f.UserId1 == UserId2 && f.Status == (int)FriendStatus.Pending);
            if (friend == null)
                return "Friend not found";

            friend.Status = (int)FriendStatus.Accepted;
            friend.UpdateAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return "";
        }

        public async Task<string> SendRequest(string UserId, string UserOtherId)
        {
            if (string.IsNullOrEmpty(UserId)) return "UserId is null";
            var isFriend = await _context.Friends.AnyAsync(f => (f.UserId1 == UserId && f.UserId2 == UserOtherId) ||
                    (f.UserId1 == UserOtherId && f.UserId2 == UserId));
            if (isFriend) return "";      // check if they are friend, return  

            var requestExists = await _context.Friends.AnyAsync(f => (f.CreateUser == UserId && f.UpdateUser == UserOtherId && f.Status == 0));
            if (requestExists) return "";  

            var newFriend = new Friend
            {
                FriendId = Guid.NewGuid().ToString(),
                UserId1 = UserId,
                UserId2 = UserOtherId,
                Status = (int)FriendStatus.Pending,
                CreateAt = DateTime.Now,
                CreateUser = UserId,
                UpdateUser = UserOtherId,
            };
            await _context.Friends.AddAsync(newFriend);
            await _context.SaveChangesAsync();

            return "";
        }
    }


}
