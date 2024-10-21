using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Services;

namespace zaloclone_test.Pages
{
    public class messageModel : PageModel
    {
        IMessageService _messageService;
        public messageModel(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        public void OnGet(string userId)
        {
            
        }
    }
}
