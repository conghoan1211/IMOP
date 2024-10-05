using AppGlobal.Common;
using zaloclone_test.Helper;
using zaloclone_test.Models;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Services
{
    public interface IAuthenticateService
    {
        public string DoLogin(UserLogin userLogin);
        public string DoRegister(UserRegister userRegister);
        public string DoLogout();
        public string DoForgetPassword(ForgetPassword input);
        public string DoVerifyOTP(VerifyOTP input, out object obj);
        public string DoResetPassword(ResetPassword input);
        public string DoChangePassword(string username, ChangePassword input);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly Zalo_CloneContext _context;
        public AuthenticateService(Zalo_CloneContext context)
        {
            _context = context;
        }

        public string DoChangePassword(string username, ChangePassword input)
        {
            throw new NotImplementedException();
        }

        public string DoForgetPassword(ForgetPassword input)
        {
            throw new NotImplementedException();
        }

        public string DoLogin(UserLogin userLogin)
        {
            throw new NotImplementedException();
        }

        public string DoLogout()
        {
            throw new NotImplementedException();
        }

        public string DoRegister(UserRegister input)
        {
            string msg = "";
            msg = _context.Users.CheckPhone(input.Phone);
            if (msg.Length > 0) return msg;

            msg = _context.Users.CheckEmail(input.Email);
            if (msg.Length > 0) return msg;

            msg = Converter.StringToMD5(input.Password, out string mkMd5);
            if (msg.Length > 0) return $"Error convert to MD5: {msg}";

            var user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Username = input.UserName,
                Phone = input.Phone,
                Email = input.Email,
                Password = mkMd5,
                RoleId = (int)Role.User,
                Sex = input.Sex,
                Dob = input.Dob,
                CreateAt = DateTime.Now,
                Status = (int)UserStatus.Inactive,
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return "";
        }

        public string DoResetPassword(ResetPassword input)
        {
            throw new NotImplementedException();
        }

        public string DoVerifyOTP(VerifyOTP input, out object obj)
        {
            throw new NotImplementedException();
        }
    }
}

