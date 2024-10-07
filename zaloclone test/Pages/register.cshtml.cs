using AppGlobal.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Services;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Pages
{
    public class registerModel : PageModel
    {
        private readonly IAuthenticateService _authenService;
        public registerModel(IAuthenticateService authenService)
        {
            _authenService = authenService;
        }

        public string MessageError { get; set; } = string.Empty;
        public string MessageSuccess { get; set; } = string.Empty;
        public bool IsRegister { get; set; } = false;
        [BindProperty]
        public UserRegister Input { get; set; }

        public async Task<IActionResult> OnPostRegister()
        {
            if (!ModelState.IsValid) return Page();

            var result = await _authenService.DoRegister(Input);
            if (!string.IsNullOrEmpty(result))
            {
                MessageError = result;
                return Page();
            }

            IsRegister = true;
            int otp = Utils.Generate6Number();
            HttpContext.Session.SetString("Otp", otp.ToString()); // Lưu OTP
            HttpContext.Session.SetString("email_verify", Input.Email); // Lưu email to verify

            MessageError = string.Empty;

            string msg = await EmailHandler.SendEmailAsync(Input.Email, "Xác thực Email của bạn", $"Đây là mã xác thực của bạn: {otp}");
            if (msg.Length > 0) MessageError = msg;

            return Page();
        }


        public async Task<IActionResult> OnPostVerifiedOTP()
        {
            IsRegister = true;
            var otp = Request.Form["InputOTP"];
            var msg = await _authenService.DoVerifyOTP(otp, HttpContext);
            if (msg.Length > 0) MessageError = msg;

            MessageSuccess = "Xác thực tài khoản thành công. Bạn có thể đăng nhập lúc này.";
            return Page();
        }
        public async Task<IActionResult> OnPostResendOTP()
        {
            IsRegister = true;
            var emailVerify = HttpContext.Session.GetString("email_verify");
            if (string.IsNullOrEmpty(emailVerify))
            {
                MessageError = "Vui lòng vào lại trang đăng nhập để được verify tài khoản.";
                return Page();
            }

            int otp = Utils.Generate6Number();
            HttpContext.Session.SetString("Otp", otp.ToString());

            string msg = await EmailHandler.SendEmailAsync(emailVerify, "Xác thực Email của bạn", $"Đây là mã xác thực của bạn: {otp}");
            if (msg.Length > 0)
            {
                MessageError = msg;
                return Page();
            }

            MessageSuccess = "Mã OTP mới đã được gửi tới email của bạn!";
            return Page();
        }

    }
}
