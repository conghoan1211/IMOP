using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Services;
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

        public async Task<IActionResult> OnPostLogin()
        {
            if (!ModelState.IsValid) return Page();

            var result = await _authenService.DoLogin(Input);
            if (!string.IsNullOrEmpty(result))
            {
                MessageError = result;
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
