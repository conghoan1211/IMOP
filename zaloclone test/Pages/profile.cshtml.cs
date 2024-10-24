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
        private readonly IAsideContactService _asideContactService;

        public profileModel(IPostService postService, JwtAuthentication jwtAuthen, IProfileService profileService, 
            IInvitationService inviteService , IAsideContactService asideContactService)
        {
            _postService = postService;
            _jwtAuthen = jwtAuthen;
            _profileService = profileService;
            _inviteService = inviteService;
            _asideContactService = asideContactService;
        }
        public string? Message { get; set; } = string.Empty;
        public bool IsFriend { get; set; }
        public List<PostVM>? ListPost { get; set; } = new();
        public ProfileVM? Profile { get; set; } = new();
        public UserToken? UserToken { get; set; }
        [BindProperty]
        public InsertUpdatePostVM? Input { get; set; }
        [BindProperty]
        public UpdateProfileModels? UpdateProfile { get; set; }
        [BindProperty]
        public UpdateAvatarVM UpdateAvatarVM { get; set; }
        public async Task<IActionResult> OnGet(string? id = null)
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
            if (UserToken == null) UserToken = userToken;

            string userid = string.IsNullOrEmpty(id) ? userToken.UserID.ToString() : id;
            if (id != null)   IsFriend = await _asideContactService.IsFriend(UserToken.UserID.ToString(), id);

            if (!string.IsNullOrEmpty(id) && id != userToken.UserID.ToString())
            {
                (msg, ListPost) = await _postService.GetPostsProfile(null, id);
            }
            else
            {
                (msg, ListPost) = await _postService.GetPostsProfile(userToken.UserID.ToString(), null);
            }

            var profile = new ProfileVM();
            (msg, profile) = await _profileService.GetProfile(userid);
            if (msg.Length > 0) Message = msg;
            if (profile == null) Message = "Không tìm thấy thông tin người dùng.";
            Profile = profile;

            (msg, UpdateProfile) = await _profileService.GetProfileUpdate(userid);
            if (msg.Length > 0) Message = msg;

            return Page();
        }

        #region Area User's post
        public async Task<IActionResult> OnPostAddPost()
        {
            if (!ModelState.IsValid)
            {
                Message = "Please correct the Model state.";
                await OnGet();
            }
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                Message = msg;
                await OnGet();
            }
            UserToken = userToken;

            msg = await _postService.InsertUpdatePost(Input, UserToken.UserID.ToString());
            if (msg.Length > 0)
            {
                Message = msg;
                await OnGet();
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
            if (UserToken == null) UserToken = userToken;
            msg = await _postService.DoPinTopPost(postId, UserToken.UserID.ToString());
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            return RedirectToPage();
        }

        private string GetUserToken()
        {
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                Message = msg;
                return msg;
            }
            UserToken = userToken;
            return "";
        }
        #endregion

        #region Area Profile crud

        public async Task<IActionResult> OnPostUpdateProfile()
        {
            ModelState.Remove(nameof(Input.Content));
            if (!ModelState.IsValid)
            {
                Message = "Please correct the Model state.";
                await OnGet();
                return Page();
            }
            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                Message = msg;
            }
            UserToken = userToken;

            msg = await _profileService.UpdateProfile(UserToken.UserID.ToString(), UpdateProfile);
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangeAvatar(string userid)
        {
            ModelState.Remove(nameof(Input.Content));
            ModelState.Remove(nameof(UpdateProfile.Sex));
            ModelState.Remove(nameof(UpdateProfile.Bio));
            ModelState.Remove(nameof(UpdateProfile.Dob));
            ModelState.Remove(nameof(UpdateProfile.UserName));

            string msg = _jwtAuthen.ParseCurrentToken(User, out UserToken userToken);
            if (msg.Length > 0)
            {
                await OnGet();
                Message = msg;
            }
            if (UserToken == null) UserToken = userToken;
            if (!ModelState.IsValid)
            {
                Message = "Please correct the Model state.";
                await OnGet();
            }

            msg = await _profileService.DoChangeAvatar(userid, UpdateAvatarVM);
            if (msg.Length > 0)
            {
                Message = msg;
                await OnGet();
            }
            return RedirectToPage();
        }

        #endregion

        #region Area Friend
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
