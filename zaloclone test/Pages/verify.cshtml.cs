using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Services;
using zaloclone_test.Utilities;

namespace zaloclone_test.Pages
{
    public class verifyModel : PageModel
    {
        private readonly IAuthenticateService _authenService;
        public verifyModel(IAuthenticateService authenService)
        {
            _authenService = authenService;
        }
        public string MessageError { get; set; } = string.Empty;
        public string MessageSuccess { get; set; } = string.Empty;
        
        public async Task<IActionResult> OnPostVerifiedOTP()
        {
            var otp = Request.Form["InputOTP"];
            var msg = await _authenService.DoVerifyOTP(otp, HttpContext);
            if (msg.Length > 0)
            {
                MessageError = msg;
                return await Task.FromResult(Page());
            }
            MessageSuccess = "Xác thực tài khoản thành công. Bạn có thể đăng nhập lúc này.";
            return await Task.FromResult(Page());
        }

        public async Task<IActionResult> OnPostResendOTP()
        {
            var emailVerify = HttpContext.Session.GetString("email_verify");
            if (string.IsNullOrEmpty(emailVerify))
            {
                MessageError = "Vui lòng vào lại trang đăng nhập để được verify tài khoản.";
                return await Task.FromResult(Page());
            }
            string msg = await EmailHandler.SendOtpAndSaveSession(emailVerify, HttpContext);
            if (msg.Length > 0)
            {
                MessageError = msg;
                return await Task.FromResult(Page());
            }

            MessageSuccess = "Mã OTP mới đã được gửi tới email của bạn!";
            return await Task.FromResult(Page());
        }
    }
}
