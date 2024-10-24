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
        public Task<(string, UpdateProfileModels)> GetProfileUpdate(string userID);
        public Task<string> DoChangeAvatar(string userid, UpdateAvatarVM input);

        public Task<string> UpdateProfile(string userID, UpdateProfileModels? updatedProfile);
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


        public async Task<string> UpdateProfile(string userID, UpdateProfileModels? updatedProfile)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userID);
            if (user == null) return "User not found";

            user.Username = updatedProfile.UserName ?? user.Username;
            user.Bio = updatedProfile.Bio ?? user.Bio;
            user.Dob = updatedProfile.Dob ?? user.Dob;
            user.Sex = updatedProfile.Sex ?? user.Sex;
            user.UpdateAt = DateTime.Now; // Cập nhật thời gian chỉnh sửa

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }

            return "";
        }

        public async Task<(string, UpdateProfileModels)> GetProfileUpdate(string userID)
        {
            var user = await _context.Users.Where(x => x.UserId == userID)
                .Select(u => new UpdateProfileModels
                {
                    UserName = u.Username,
                    Bio = u.Bio,
                    Dob = u.Dob,
                    Sex = u.Sex,
                }).FirstOrDefaultAsync();
            if (user == null) return ("User not found", null);
            return ("", user);
        }

        public async Task<string> DoChangeAvatar(string userid, UpdateAvatarVM input)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userid);
            if (user == null) return "User not found";

            var files = input.Image;
            if (files == null) return "";

            var (msg, fileName) = await Common.GetUrlImage(files);
            if (msg.Length > 0) return msg;

            user.Avatar = fileName;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return "";
        }
    }
}