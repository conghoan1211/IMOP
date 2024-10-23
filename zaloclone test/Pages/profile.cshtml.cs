using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using zaloclone_test.Models;
using zaloclone_test.Services;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;
using zaloclone_test.ViewModels.Token;

namespace zaloclone_test.Pages
{
    [Authorize]
    public class profileModel : PageModel
    {
        private readonly JwtAuthentication _jwtAuthen;
        private readonly IPostService _postService;
        private readonly IProfileService _profileService;
        private readonly IInvitationService _inviteService;

        public profileModel(IPostService postService, JwtAuthentication jwtAuthen, IProfileService profileService, IInvitationService inviteService)
        {
            _postService = postService;
            _jwtAuthen = jwtAuthen;
            _profileService = profileService;
            _inviteService = inviteService;
        }
        public string Message { get; set; } = string.Empty;
        public List<PostVM>? listPost { get; set; } = new();
        public ProfileVM? Profile { get; set; } = new();

        [BindProperty]
        public InsertUpdatePostVM Input { get; set; }
        public UserToken UserToken { get; set; }

        public async Task OnGet(string? id = null)
        {
            if (!ModelState.IsValid)
            {
                Message = "Please correct the Model state.";
            }
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                Message = msg;
            }
            UserToken = userToken;

            string userid = string.IsNullOrEmpty(id) ? userToken.UserID.ToString() : id;
            if (!string.IsNullOrEmpty(id) && id != userToken.UserID.ToString())
            {
                (msg, listPost) = await _postService.GetPostsProfile(null, id);
            }
            else
            {
                (msg, listPost) = await _postService.GetPostsProfile(userToken.UserID.ToString(), null);
            }
            var profile = new ProfileVM();
            (msg, profile) = await _profileService.GetProfile(userid);
            if (msg.Length > 0) Message = msg;
            if (profile == null) Message = "Không tìm thấy thông tin người dùng.";
            Profile = profile;
        }

        #region Area User's post
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = "Please correct the Model state.";
                return Page();
            }
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            UserToken = userToken;

            msg = await _postService.InsertUpdatePost(Input, UserToken.UserID.ToString());
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAllowComment(string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                Message = "Post ID không hợp lệ.";
                return RedirectToPage();
            }
            string msg = await _postService.DoAllowComment(postId);
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeletePost(string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                Message = "Post ID không hợp lệ.";
                return Page();
            }
            string msg = await _postService.DoDeletePost(postId);
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostPinTop(string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                Message = "Post ID không hợp lệ.";
                return Page();
            }
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            UserToken = userToken;
            msg = await _postService.DoPinTopPost(postId, UserToken.UserID.ToString());
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            return RedirectToPage();
        }
        #endregion

        #region Area Profile crud

        #endregion

        #region Area add user
        public async Task<IActionResult> OnPostSendRequest(string otherUserId)
        {
            if (string.IsNullOrEmpty(otherUserId))
            {
                Message = "User request Id không hợp lệ.";
                return RedirectToPage();
            }
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            UserToken = userToken;
            msg = await _inviteService.SendRequest(userToken.UserID.ToString(), otherUserId);
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            return RedirectToPage();

        }
        #endregion

    }
}
