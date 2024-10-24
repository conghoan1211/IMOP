using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using zaloclone_test.Models;
using zaloclone_test.Services;
using zaloclone_test.ViewModels;

namespace Server.Pages
{
    public class aside_invitationsModel : PageModel
    {
        private readonly IInvitationService _inviteService;
        public aside_invitationsModel(IInvitationService inviteService)
        {
            _inviteService = inviteService;
        }

        public IList<InvitationVM> allInvitation { get; set; }
        public IList<InvitationVM> sentInvitation { get; set; }
        public string MessageError { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.Claims;
            string UserId = claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            if(!string.IsNullOrEmpty(UserId))
            {
                var (message, allUser) = await _inviteService.GetAllInvitation(UserId);
                var (sentmessage, sentUser) = await _inviteService.GetAllRequested(UserId);

                if (message.Length > 0)
                {
                    MessageError = message;
                    return Page();
                }
                else if (message.Length == 0 && allUser != null)
                {
                    allInvitation = allUser;
                }
                if (sentmessage.Length > 0)
                {
                    MessageError = sentmessage;
                    return Page();
                }
                else if (sentmessage.Length == 0 && sentUser != null)
                {
                    sentInvitation = sentUser;
                }
            }

            return Page();
        }

        public string UserId { get; set; }
        public async Task<IActionResult> OnPostInvoke()
        {
            UserId = Request.Form["Id"];
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.Claims;
            string UserId1 = claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            if (!string.IsNullOrEmpty(UserId1))
            {
                var message = await _inviteService.RevokeInvitation(UserId1, UserId);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteInvitation()
        {
            UserId = Request.Form["UserId"];
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.Claims;
            string UserId1 = claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            if (!string.IsNullOrEmpty(UserId1))
            {
                var message = await _inviteService.RevokeInvitation(UserId1, UserId);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAcceptInvitation()
        {
            UserId = Request.Form["UserId"];
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.Claims;
            string UserId1 = claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            if (!string.IsNullOrEmpty(UserId1))
            {
                var message = await _inviteService.AcceptInvitation(UserId1, UserId);
            }
            return RedirectToPage();
        }
    }
}
