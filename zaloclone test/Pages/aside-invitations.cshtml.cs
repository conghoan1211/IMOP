using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using zaloclone_test.Models;
using zaloclone_test.Services;

namespace Server.Pages
{
    public class aside_invitationsModel : PageModel
    {
        private readonly IInvitationService _inviteService;
        public aside_invitationsModel(IInvitationService inviteService)
        {
            _inviteService = inviteService;
        }
        public string UserID { get; set; }

        public IList<Friend> allInvitation { get; set; }

        public async Task<IActionResult> OnGetInvitationAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.Claims;
            string UserId = claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            if(!string.IsNullOrEmpty(UserId))
            {
                var (message, allUser) = await _inviteService.GetAllInvitation(UserId);
                if(message.Length == 0 && allUser != null)
                {
                    allInvitation = allUser;
                }
            }

            return RedirectToPage("/aside-invitations");
        }
    }
}
