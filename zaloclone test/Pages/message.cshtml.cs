using Htmx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Models;
using zaloclone_test.Services;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels.Token;

namespace zaloclone_test.Pages
{
    public class messageModel : PageModel
    {
        private readonly IMessageService _messageService;
        private readonly JwtAuthentication _authentication;
        [BindProperty] public UserToken LoggedInUser { get; set; }
        [BindProperty] public List<Conversation> Conversations { get; set; }
        public messageModel(IMessageService messageService, JwtAuthentication authentication)
        {
            _messageService = messageService;
            _authentication = authentication;
            Conversations = new List<Conversation>();
        }
        public IActionResult OnGet(string? conversationId)
        {
            string? jwtToken = HttpContext.Request.Cookies["JwtToken"];
            if (jwtToken != null)
            {
                LoggedInUser = _authentication.ParseJwtToken(jwtToken);
            }
            
            return Request.IsHtmx() 
                ? Content($"<h1></h1>", "text/html") 
                : Page();
        }
    }
}
