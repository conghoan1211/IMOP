using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using zaloclone_test.Services;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Pages
{
    public class headerModel : PageModel
    {
        private readonly IAuthenticateService _authenService;
        private readonly IProfile _profileService;

        public headerModel(IAuthenticateService authenService, IProfile profileService)
        {
            _authenService = authenService;
            _profileService = profileService;
        }

        public ProfileViewModels UserProfile { get; set; }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            var message = await _authenService.DoLogout(HttpContext);
            return RedirectToPage("/login");
        }

        public async Task OnGetAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.Claims;

            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                UserProfile = await _profileService.ViewProfile(userId);
            }
        }
    }
}