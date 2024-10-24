using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Helper;
using zaloclone_test.Services;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;

namespace Server.Pages
{
    public class loginModel : PageModel
    {
        private readonly IAuthenticateService _authenService;
        public loginModel(IAuthenticateService authenService)
        {
            _authenService = authenService;
        }

        public string MessageError { get; set; } = string.Empty;
        public string MessageSuccess { get; set; } = string.Empty;
        [BindProperty]
        public UserLogin Input { get; set; }
        [BindProperty]
        public ForgetPassword Forgot { get; set; }

        public async Task<IActionResult> OnPostLogin()
        {
            ModelState.Remove(nameof(Forgot.Email));
            if (!ModelState.IsValid)
                return Page(); // No need to await

            var result = await _authenService.DoLogin(Input, HttpContext);

            if (result.Equals(ConstMessage.ACCOUNT_UNVERIFIED))
            {
                var (msg, user) = await _authenService.DoSearchByPhone(Input.Phone);
                if (msg.Length > 0)
                {
                    MessageError = result;
                    return await Task.FromResult(Page());
                }
                msg = await EmailHandler.SendOtpAndSaveSession(user.Email, HttpContext);
                if (msg.Length > 0)
                {
                    MessageError = msg;
                    return await Task.FromResult(Page());
                }

                TempData["MsOtp"] = "Xác thực tài khoản Email trước khi đăng nhập.\n Nhập mã OTP từ Email của bạn.";
                return RedirectToPage("./verify");
            }
            else if (!string.IsNullOrEmpty(result))
            {
                MessageError = result;
                return await Task.FromResult(Page());
            }

            return RedirectToPage("./post");
        }

        public async Task<IActionResult> OnPostResetPass()
        {
            ModelState.Remove(nameof(Input.Phone));
            ModelState.Remove(nameof(Input.Password));

            if (!ModelState.IsValid)
                return Page();

            string msg = await _authenService.DoForgetPassword(Forgot, HttpContext);
            if (msg.Length > 0)
            {
                MessageError = msg;
                return await Task.FromResult(Page());
            }

            MessageSuccess = "Mật khẩu mới đã được gửi về Email của bạn.";
            return await Task.FromResult(Page());
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            string phone = User.Claims.FirstOrDefault(c => c.Type == "Phone")?.Value;

            if (!string.IsNullOrEmpty(phone))
            {
                var message = await _authenService.DoLogout(HttpContext, phone);
                TempData["MsLogout"] = message;
            }
            return RedirectToPage("/login");
        }
    }
}
