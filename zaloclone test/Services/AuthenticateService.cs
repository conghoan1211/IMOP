using AppGlobal.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using zaloclone_test.Helper;
using zaloclone_test.Models;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Services
{
    public interface IAuthenticateService
    {
        public Task<string> DoLogin(UserLogin userLogin, HttpContext httpContext);
        public Task<string> DoRegister(UserRegister userRegister);
        public Task<string> DoLogout(HttpContext httpContext);
        public Task<string> DoForgetPassword(ForgetPassword input, HttpContext httpContext);
        public Task<string> DoVerifyOTP(string otp, HttpContext httpContext);
        public Task<string> DoResetPassword(ResetPassword input);
        public Task<string> DoChangePassword(string username, ChangePassword input);
        public Task<(string message, User? user)> DoSearchByEmail(string? email);
        public Task<(string message, User? user)> DoSearchByPhone(string? phone);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly Zalo_CloneContext _context;
        private readonly JwtAuthentication _jwtAuthen;
        public AuthenticateService(Zalo_CloneContext context, JwtAuthentication jwtAuthen)
        {
            _context = context;
            _jwtAuthen = jwtAuthen;
        }

        public Task<string> DoChangePassword(string username, ChangePassword input)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DoForgetPassword(ForgetPassword input, HttpContext httpContext)
        {
            var (msg, user) = await DoSearchByEmail(input.Email);
            if (msg.Length > 0) return msg;
            else if (user != null)
            {
                string newpass = "";
                (msg, newpass) = await EmailHandler.SendPasswordAndSaveSession(input.Email, httpContext);
                if (msg.Length > 0) return msg;

                httpContext.Session.Remove("newPassword");
                msg = Converter.StringToMD5(newpass, out string mkMd5);
                if (msg.Length > 0) return $"Error convert to MD5: {msg}";

                user.Password = mkMd5;
                user.UpdateUser = user.UserId;
                user.UpdateAt = DateTime.Now;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return "";
        }

        public async Task<string> DoLogin(UserLogin userLogin, HttpContext httpContext)
        {
            var (msg, user) = await DoSearchByPhone(userLogin.Phone);
            if (msg.Length > 0) return msg;

            msg = Converter.StringToMD5(userLogin.Password, out string mkMd5);
            if (msg.Length > 0) return $"Error convert to MD5: {msg}";
            if (!user.Password.ToUpper().Equals(mkMd5.ToUpper())) return "Mật khẩu không chính xác";

            if (user.IsVerified == false) return ConstMessage.ACCOUNT_UNVERIFIED;
            if (user.IsActive == false) return "Tài khoản đã bị khóa.";

            //create token here...
            var token = _jwtAuthen.GenerateJwtToken(user);
            httpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.Strict // Prevent cross-site attacks
            });
            return "";
        }

        public Task<string> DoLogout(HttpContext httpContext)
        {
            // Xóa cookie chứa JWT token
            httpContext.Response.Cookies.Delete("JwtToken");

            // Nếu có session, bạn có thể xóa session tại đây (tùy theo yêu cầu ứng dụng của bạn)
            httpContext.Session.Clear();

            return Task.FromResult("Đăng xuất thành công.");
        }


        public async Task<string> DoRegister(UserRegister input)
        {
            string msg = "";
            msg = _context.Users.CheckPhone(input.Phone);
            if (msg.Length > 0) return msg;

            msg = _context.Users.CheckEmail(input.Email);
            if (msg.Length > 0) return msg;

            msg = Converter.StringToMD5(input.Password, out string mkMd5);
            if (msg.Length > 0) return $"Error convert to MD5: {msg}";

            var userid = Guid.NewGuid().ToString();
            var user = new User
            {
                UserId = userid,
                Username = input.UserName,
                Phone = input.Phone,
                Email = input.Email,
                Password = mkMd5,
                RoleId = (int)Role.User,
                Sex = input.Sex,
                Dob = input.Dob,
                Status = (int)UserStatus.Inactive,
                IsActive = true,
                IsDisable = false,
                IsVerified = false,
                CreateAt = DateTime.Now,
                CreateUser = userid,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "";
        }

        public Task<string> DoResetPassword(ResetPassword input)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DoVerifyOTP(string otp, HttpContext httpContext)
        {
            var storedOtp = httpContext.Session.GetString("Otp");
            if (string.IsNullOrEmpty(storedOtp)) return "OTP đã hết hạn";
            if (otp != storedOtp) return "Mã OTP nhập không hợp lệ!";

            var emailVerify = httpContext.Session.GetString("email_verify");
            if (string.IsNullOrEmpty(emailVerify)) return "Vui lòng đăng nhập lại để được verify tài khoản.";

            var (msg, user) = await DoSearchByEmail(emailVerify);
            if (msg.Length > 0) return msg;
            else if (user != null)
            {
                user.IsVerified = true;
                user.UpdateAt = DateTime.Now;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            httpContext.Session.Remove("Otp");
            httpContext.Session.Remove("email_verify");

            return string.Empty;
        }

        public async Task<(string message, User? user)> DoSearchByEmail(string? email)
        {
            if (string.IsNullOrEmpty(email) || !email.IsValidEmailFormat())
                return ("Email không hợp lệ", null);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return ("Tài khoản Email không tồn tại.", null);

            return (string.Empty, user);
        }

        public async Task<(string message, User? user)> DoSearchByPhone(string? phone)
        {
            if (string.IsNullOrEmpty(phone)) return ("Số điện thoại không hợp lệ", null);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Phone == phone);
            if (user == null) return ("Tài khoản không tồn tại.", null);

            return (string.Empty, user);
        }

    }
}

