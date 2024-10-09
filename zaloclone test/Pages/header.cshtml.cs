using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using zaloclone_test.Services;

namespace zaloclone_test.Pages
{
    public class headerModel : PageModel
    {
        private readonly IAuthenticateService _authenService;
        public headerModel(IAuthenticateService authenService)
        {
            _authenService = authenService;
        }
        public string UserID { get; set; }


        public async Task<IActionResult> OnPostLogoutAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.Claims;
            string Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var message = await _authenService.DoLogout(HttpContext);
            return RedirectToPage("/login");
        }

        public void OnGet()
        {
            // Lấy thông tin từ JWT
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.Claims;

            // Lấy thông tin từ các claims
            UserID = claims.FirstOrDefault(c => c.Type == "UserID")?.Value;


        }
    }
}
