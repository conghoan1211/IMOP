using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Models;
using zaloclone_test.Services;
using zaloclone_test.ViewModels;

namespace Server.Pages
{
    public class registerModel : PageModel
    {

        private readonly IAuthenticateService _authenService; // Inject interface thay vì class trực tiếp
        public registerModel(IAuthenticateService authenService)
        {
            _authenService = authenService;
        }
        [BindProperty]
        public UserRegister Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostRegister()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _authenService.DoRegister(Input);

            if (!string.IsNullOrEmpty(result))
            {
                ModelState.AddModelError(string.Empty, result);
                return Page();
            }

            // Redirect to a success page or login page after successful registration
            return RedirectToPage("Login");
        }
    }
}
