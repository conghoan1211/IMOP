using AppGlobal.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using zaloclone_test.Helper;
using zaloclone_test.Models;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Services
{
    public interface IProfileService
    {
        public Task<(string msg, ProfileVM? result)> GetProfile(string userID);
        public Task<(string, UpdateProfileModels?)> GetProfileUpdate(string userID);
        public Task<string> DoChangeAvatar(string userid, UpdateAvatarVM input, HttpContext http);

        public Task<string> UpdateProfile(string userID, UpdateProfileModels? updatedProfile, HttpContext http);
    }
    public class ProfileService : IProfileService
    {
        private readonly Zalo_CloneContext _context;
        private readonly JwtAuthentication _jwtAuthen;
        public ProfileService(Zalo_CloneContext context, JwtAuthentication jwtAuthen)
        {
            _context = context;
            _jwtAuthen = jwtAuthen;
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


        public async Task<string> UpdateProfile(string userID, UpdateProfileModels? updatedProfile, HttpContext http)
        {
            if (updatedProfile == null) return "Dự liệu ko hợp lệ.";
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userID);
            if (user == null) return "User not found";

            var oldProfile = new UpdateProfileModels
            {
                UserName = user.Username,
                Sex = user.Sex,
                Dob = user.Dob,
                Bio = user.Bio
            };

            oldProfile.UserName = updatedProfile.UserName;
            oldProfile.Bio = updatedProfile.Bio;
            oldProfile.Dob = updatedProfile.Dob;
            oldProfile.Sex = updatedProfile.Sex;

            if (oldProfile.AreObjectsDifferent(updatedProfile))
                return "";   // check if nothing change, return 
            try
            {
                user.Sex = updatedProfile.Sex;
                user.Dob = updatedProfile.Dob;
                user.Bio = updatedProfile.Bio;
                user.Username = updatedProfile.UserName;
                user.UpdateAt = DateTime.Now;  
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }

            http.Response.Cookies.Delete("JwtToken");         // update data token
            var token = _jwtAuthen.GenerateJwtToken(user);
            http.Response.Cookies.Append("JwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
            return "";
        }

        public async Task<(string, UpdateProfileModels?)> GetProfileUpdate(string userID)
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

        public async Task<string> DoChangeAvatar(string userid, UpdateAvatarVM input, HttpContext http)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userid);
            if (user == null) return "User not found";

            var files = input.Image;
            if (files == null) return "";

            var (msg, fileName) = await Common.GetUrlImage(files);
            if (msg.Length > 0) return msg;

            var oldAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), Constant.UrlImagePath, user.Avatar);
            try
            {
                user.Avatar = fileName;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                if (File.Exists(oldAvatarPath))  File.Delete(oldAvatarPath);

                // Update the JWT token
                http.Response.Cookies.Delete("JwtToken"); // Remove the old token
                var token = _jwtAuthen.GenerateJwtToken(user); // Generate a new token
                http.Response.Cookies.Append("JwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
            }
            catch (Exception ex)
            {
                return $"An error occurred while updating the avatar: {ex.Message}";
            }
            return "";
        }
    }
}