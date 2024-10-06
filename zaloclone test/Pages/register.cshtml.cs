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
        public string MessageError { get; set; } = string.Empty;
        public bool IsRegister { get; set; } = false;
        [BindProperty]
        public UserRegister Input { get; set; }

        public registerModel(IAuthenticateService authenService)
        {
            _authenService = authenService;
        }

        public IActionResult OnPostRegister()
        {
            if (!ModelState.IsValid) return Page();

            var result = _authenService.DoRegister(Input);
            if (!string.IsNullOrEmpty(result))
            {
                MessageError = result;
                return Page();
            }

            IsRegister = true;
            int otp = Utils.Generate6Number();
            HttpContext.Session.SetString("Otp", otp.ToString()); // Lưu OTP

            string msg = EmailHandler.SendEmail(Input.Email, "Xác thực Email của bạn", $"Đây là mã xác thực của bạn: {otp}");
            if (msg.Length > 0)
            {
                MessageError = result;
            }
            return Page();

            //  return RedirectToPage("Login");
        }
    }
}
