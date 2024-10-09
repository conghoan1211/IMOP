using zaloclone_test.ViewModels;
using zaloclone_test.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace zaloclone_test.Services
{
    public interface IProfile
    {
       public Task<ProfileViewModels> ViewProfile(string userId); // Trả về ProfileViewModels theo UserId
       public Task<(bool Success, string Message)> UpdateProfile(UpdateProfileModels updateProfile); // Sửa tên thành UpdateProfilModels
    }
    public class ProfileService : IProfile
    {
        private readonly Zalo_CloneContext _context;
        public ProfileService(Zalo_CloneContext context)
        {
            _context = context;
        }
        public async Task<ProfileViewModels> ViewProfile(string userId)
        {
            // Lấy dữ liệu người dùng từ cơ sở dữ liệu
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return null; // Người dùng không tồn tại
            }
            // Trả về thông tin người dùng trong ProfileViewModels
            return new ProfileViewModels
            {
                UserId = user.UserId,
                Username = user.Username,
                Sex = (int)user.Sex,
                Dob = (DateTime)user.Dob,
                Bio = user.Bio,
                AvatarUrl = user.Avatar,
                // Thêm các thuộc tính khác nếu cần
            };
        }
        public async Task<(bool Success, string Message)> UpdateProfile(UpdateProfileModels updateProfile)
        {
            // Kiểm tra dữ liệu đầu vào
            if (updateProfile == null)
            {
                return (false, "Dữ liệu không hợp lệ!");
            }
            // Lấy người dùng từ cơ sở dữ liệu
            var user = await _context.Users.FindAsync(updateProfile.UserId);
            if (user == null)
            {
                return (false, "Người dùng không tồn tại!");
            }
            // Cập nhật dữ liệu từ form
            user.Username = updateProfile.UserName;
            user.Sex = updateProfile.Sex;
            user.Dob = updateProfile.Dob;
            user.Bio = updateProfile.Bio;
            // Lưu thay đổi vào cơ sở dữ liệu
            var saveResult = await _context.SaveChangesAsync();
            // Kiểm tra xem có thay đổi nào được lưu không
            if (saveResult > 0)
            {
                return (true, "Thông tin cá nhân đã được cập nhật thành công!");
            }
            else
            {
                return (false, "Không có thay đổi nào được thực hiện.");
            }
        }
    }
}