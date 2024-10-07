using AppGlobal.Common;
using Microsoft.EntityFrameworkCore;
using zaloclone_test.Helper;
using zaloclone_test.Models;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Services
{
    public interface IAuthenticateService
    {
        public Task<string> DoLogin(UserLogin userLogin);
        public Task<string> DoRegister(UserRegister userRegister);
        public Task<string> DoLogout();
        public Task<string> DoForgetPassword(ForgetPassword input);
        public Task<string> DoVerifyOTP(string otp, HttpContext httpContext);
        public Task<string> DoResetPassword(ResetPassword input);
        public Task<string> DoChangePassword(string username, ChangePassword input);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly Zalo_CloneContext _context;
        public AuthenticateService(Zalo_CloneContext context)
        {
            _context = context;
        }

        public Task<string> DoChangePassword(string username, ChangePassword input)
        {
            throw new NotImplementedException();
        }

        public Task<string> DoForgetPassword(ForgetPassword input)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DoLogin(UserLogin userLogin)
        {
            var (msg, user) = await DoSearchByPhone(userLogin.Phone);
            if (msg.Length > 0) return msg;

            msg = Converter.StringToMD5(userLogin.Password, out string mkMd5);
            if (msg.Length > 0) return $"Error convert to MD5: {msg}";
            if (!user.Password.Equals(mkMd5)) return "Mật khẩu không chính xác";
           
            //create token here...

            return "";
        }

        public Task<string> DoLogout()
        {
            throw new NotImplementedException();
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
                CreateAt = DateTime.Now,
                CreateUser = userid,
                Status = (int)UserStatus.Inactive,
                IsActive = true,
                IsDisable = false,
                IsVerified = false,
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
            if (string.IsNullOrEmpty(emailVerify)) return "Vui lòng vào trang đăng nhập để được verify tài khoản.";

            var (msg, user) = await DoSearchByEmail(emailVerify);
            if (msg.Length > 0) return msg;
            else if (user != null)
            {
                user.IsVerified = true;
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
            if (user == null)
                return ("Tài khoản Email không tồn tại.", null);

            return (string.Empty, user);
        }
        public async Task<(string message, User? user)> DoSearchByPhone(string? phone)
        {
            if (string.IsNullOrEmpty(phone)) return ("Số điện thoại không hợp lệ", null);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Phone == phone);
            if (user == null)
                return ("Tài khoản không tồn tại.", null);

            return (string.Empty, user);
        }

    }
}

