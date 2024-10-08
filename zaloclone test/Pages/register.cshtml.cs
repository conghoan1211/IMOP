using AppGlobal.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Models;
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
        [BindProperty]
        public UserRegister Input { get; set; }

        public async Task<IActionResult> OnPostRegister()
        {
            if (!ModelState.IsValid) return await Task.FromResult(Page());

            var result = await _authenService.DoRegister(Input);
            if (!string.IsNullOrEmpty(result))
            {
                MessageError = result;
                return await Task.FromResult(Page());
            }

            string msg = await EmailHandler.SendOtpAndSaveSession(Input.Email, HttpContext);
            if (msg.Length > 0)
            {
                MessageError = msg;
                return await Task.FromResult(Page());
            }
            TempData["MsOtp"] = "Mã OTP đã được gửi về Email của bạn.";
            return RedirectToPage("./verify");
        }
    }
}
