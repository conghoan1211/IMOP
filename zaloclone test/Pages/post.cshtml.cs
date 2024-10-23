using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.ViewModels.Token;
using zaloclone_test.ViewModels;
using zaloclone_test.Services;
using zaloclone_test.Utilities;
using zaloclone_test.Models;
using Microsoft.AspNetCore.SignalR;
using zaloclone_test.MyHub;

namespace zaloclone_test.Pages
{
    public class postModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly JwtAuthentication _jwtAuthen;

        public postModel(IPostService postService, JwtAuthentication jwtAuthen)
        {
            _postService = postService;
            _jwtAuthen = jwtAuthen;
        }
        public string Message { get; set; } = string.Empty;
        public List<PostVM>? listPost { get; set; } = new();

        [BindProperty]
        public InsertUpdatePostVM? Input { get; set; }
        public UserToken? UserToken { get; set; }

        public async Task OnGet()
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
            var (message, result) = await _postService.GetListPosts();
            if (message.Length > 0) Message = msg;
            listPost = result;
        }

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

        public async Task<IActionResult> OnPostToggleLike(string postId)
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

            int? countLikes = 0;
            (msg, countLikes) = await _postService.DoLikePost(postId, userToken.UserID.ToString());
            if (msg.Length > 0)
            {
                Message = msg;
                return Page();
            }

            //var hubContext = HttpContext.RequestServices.GetService<IHubContext<PostHub>>();
            //await hubContext.Clients.All.SendAsync("ReceiveLikeUpdate", postId, countLikes);

            return RedirectToPage();
        }
    }
}
