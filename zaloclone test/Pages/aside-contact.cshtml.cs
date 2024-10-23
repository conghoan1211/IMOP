using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using zaloclone_test.Services;
using zaloclone_test.ViewModels;

namespace Server.Pages
{
    public class aside_contactModel : PageModel
    {
        private readonly IAsideContactService _asideContactService;

        public aside_contactModel(IAsideContactService asideContactService)
        {
            _asideContactService = asideContactService;
        }

        [BindProperty]
        public List<AsideContactVM> Friends { get; set; }

        [BindProperty]
        public FriendFilterModel FilterModel { get; set; }

        [BindProperty]
        public BlockFriendModel BlockModel { get; set; }

        [BindProperty]
        public FriendProfileModel SelectedProfile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/login");
            }

            Friends = await _asideContactService.GetFriendsList(userId);
            return Page();
        }

        public async Task<IActionResult> OnPostFilterAsync([FromBody] FriendFilterModel filter)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new JsonResult(new { success = false, message = "Unauthorized" });
            }

            try
            {
                var filteredFriends = await _asideContactService.FilterFriends(userId, filter);
                return new JsonResult(new { success = true, data = filteredFriends });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> OnPostDeleteFriendAsync([FromBody] string friendId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new JsonResult(new { success = false, message = "Unauthorized" });
            }

            try
            {
                var result = await _asideContactService.DeleteFriend(userId, friendId);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> OnPostBlockFriendAsync([FromBody] BlockFriendModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new JsonResult(new { success = false, message = "Unauthorized" });
            }

            try
            {
                var result = await _asideContactService.BlockFriend(userId, model);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> OnGetFriendProfileAsync(string friendId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new JsonResult(new { success = false, message = "Unauthorized" });
            }

            try
            {
                var profile = await _asideContactService.GetFriendProfile(userId, friendId);
                return new JsonResult(new { success = true, data = profile });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
    }
}
