using Microsoft.EntityFrameworkCore;
using zaloclone_test.Helper;
using zaloclone_test.Models;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Services
{
    public interface IProfileService
    {
        public Task<(string msg, ProfileVM? result)> GetProfile(string userID);

    }
    public class ProfileService : IProfileService
    {
        private readonly Zalo_CloneContext _context;
        public ProfileService(Zalo_CloneContext context)
        {
            _context = context;
        }
        public async Task<(string msg, ProfileVM? result)> GetProfile(string userID)
        {
            var user = await _context.Users.Where(x => x.UserId == userID)
                .Select(u => new ProfileVM
                {
                    UserID = u.UserId,
                    UserName = u.Username,
                    Phone = u.Phone,
                    Email = u.Email,
                    Avatar = u.Avatar,
                    Bio = u.Bio,
                    Dob = u.Dob,
                    Sex = u.Sex,
                    RoleID = u.RoleId,
                    IsDisable = u.IsDisable,
                    IsActive = u.IsActive ?? false,
                    IsVerified = u.IsVerified ?? false,
                    CreateAt = u.CreateAt,
                    CreateUser = u.CreateUser,
                    UpdateAt = u.UpdateAt,
                    UpdateUser = u.UpdateUser,
                    Status = u.Status,
                }).FirstOrDefaultAsync();
            if (user == null) return ("User not found", null);

            var friendCount = await _context.Friends.Where(f => f.Status == (int)FriendStatus.Accepted && (f.UserId1 == userID || f.UserId2 == userID)).CountAsync();
            if (friendCount > 0) user.NumberOfFriends = friendCount;

            return (string.Empty, user);
        }
    }
}