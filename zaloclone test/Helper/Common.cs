using AppGlobal.Common;
using System.Text.RegularExpressions;
using zaloclone_test.Models;

namespace zaloclone_test.Helper
{
    public static class Common
    {
        public static string PreparePhone(this string phone, out object result)
        {
            result = null;
            if (string.IsNullOrEmpty(phone)) return "Số điện thoại không được để trống";

            result = phone.Trim().Standardizing().RemoveWhitespace();
            return "";
        }

        public static string CheckPhone(this IQueryable<User> query, string phone)
        {
            string msg = phone.PreparePhone(out object result);
            if (msg.Length > 0) return msg;
            if (result.ObjToString().Length != 10) return "Số điện thoại phải có 10 chữ số";

            bool check = query.Any(u => u.Phone.Contains(result.ObjToString()));
            if (check) return "Số điện thoại đã được sử dụng";
            return "";
        }

        public static string PrepareEmail(this string email, out object result)
        {
            result = null;
            if (string.IsNullOrEmpty(email)) return "Email không được để trống";

            result = email.Trim().RemoveWhitespace(); // Chuẩn hóa email
            return "";
        }

        public static bool IsValidEmailFormat(this string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Định dạng email cơ bản
            return Regex.IsMatch(email, emailPattern);
        }

        public static string CheckEmail(this IQueryable<User> query, string email)
        {
            string msg = email.PrepareEmail(out object result);
            if (msg.Length > 0) return msg;
            if (!result.ObjToString().IsValidEmailFormat()) return "Email không đúng định dạng";

            bool check = query.Any(u => u.Email.ToLower() == result.ObjToString().ToLower());
            if (check) return ConstMessage.EMAIL_EXISTED;

            return "";
        }
        public static async Task<(string, string?)> GetUrlImage(IFormFile file)
        {
            var (error , fileNames) = await GetUrlImages(new[] { file });
            if (!string.IsNullOrEmpty(error))
            {
                return (error, null);
            }

            return (string.Empty, fileNames?.FirstOrDefault());
        }

        public static async Task<(string, List<string>?)> GetUrlImages(IFormFile[] files)
        {
            List<string> fileNames = new List<string>();
            var allowedExtensions = Constant.IMAGE_EXTENDS; // Định dạng file được phép

            foreach (var file in files)
            {
                if (file.Length > 1048576) // Giới hạn kích thước 1MB
                {
                    return ("The file is too large.", null);
                }

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    return ("Invalid file format.", null);
                }

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), Constant.UrlImagePath);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + extension; // Tạo tên file duy nhất
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                fileNames.Add(uniqueFileName);
            }

            return (string.Empty, fileNames);
        }

    }
}
