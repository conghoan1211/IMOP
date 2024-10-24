using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Services;
using zaloclone_test.ViewModels;
using zaloclone_test.ViewModels.Token;
using zaloclone_test.Utilities;

namespace Server.Pages
{
    public class aside_contactModel : PageModel
    {
        private readonly IAsideContactService _asideContactService;
        private readonly JwtAuthentication _jwtAuthen;

        public aside_contactModel(IAsideContactService asideContactService, JwtAuthentication jwtAuthen)
        {
            _asideContactService = asideContactService;
            _jwtAuthen = jwtAuthen;
        }

        [BindProperty]
        public List<AsideContactVM> Friends { get; set; }

        public UserToken UserToken { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                return RedirectToPage("/login");
            }
            UserToken = userToken;

            Friends = await _asideContactService.GetFriendsList(UserToken.UserID.ToString());
            return Page();
        }

        public async Task<IActionResult> OnPostFilterAsync([FromBody] FriendFilterModel filter)
        {
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                return new JsonResult(new { success = false, message = "Unauthorized" });
            }
            UserToken = userToken;

            try
            {
                var filteredFriends = await _asideContactService.FilterFriends(UserToken.UserID.ToString(), filter);
                return new JsonResult(new { success = true, data = filteredFriends });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> OnPostDeleteFriendAsync([FromBody] DeleteFriendModel model)
        {
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                return new JsonResult(new { success = false, message = "Unauthorized" });
            }
            UserToken = userToken;

            try
            {
                var result = await _asideContactService.DeleteFriend(UserToken.UserID.ToString(), model.FriendId);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> OnPostBlockFriendAsync([FromBody] BlockFriendModel model)
        {
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                return new JsonResult(new { success = false, message = "Unauthorized" });
            }
            UserToken = userToken;

            try
            {
                var result = await _asideContactService.BlockFriend(UserToken.UserID.ToString(), model);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> OnPostUnblockFriendAsync([FromBody] BlockFriendModel model)
        {
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                return new JsonResult(new { success = false, message = "Unauthorized" });
            }
            UserToken = userToken;

            try
            {
                var result = await _asideContactService.UnblockFriend(UserToken.UserID.ToString(), model.UserId);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
    }

    public class DeleteFriendModel
    {
        public string FriendId { get; set; }
    }
}