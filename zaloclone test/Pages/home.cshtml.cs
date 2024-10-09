using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using zaloclone_test.Services;

namespace zaloclone_test.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly IAuthenticateService _authenService;
        public HomeModel(IAuthenticateService authenService)
        {
            _authenService = authenService;
        }
        public string Username { get; set; }
        public string Email { get; set; }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            var message = await _authenService.DoLogout(HttpContext);
            return RedirectToPage("/login");
        }

        public void OnGet()
        {
            // Lấy thông tin từ JWT
            var claimsIdentity = (ClaimsIdentity)User.Identity;
      
            var claims = claimsIdentity.Claims;

            // Lấy thông tin từ các claims
            Username = claims.FirstOrDefault(c => c.Type == "Username")?.Value;
            Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            // Sử dụng Username và Email trong ViewModel
        }
    }
}
