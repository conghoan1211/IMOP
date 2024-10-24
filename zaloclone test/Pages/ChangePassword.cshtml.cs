using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Services;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IAuthenticateService _authenService;
        private readonly JwtAuthentication _jwtAuthen;

        public ChangePasswordModel(IAuthenticateService authenService, JwtAuthentication jwtAuthen)
        {
            _jwtAuthen = jwtAuthen;
            _authenService = authenService;
        }

        [BindProperty]
        public ChangePassword Input { get; set; }
        public string MessageError { get; set; } = string.Empty;
        public string MessageSuccess { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            var token = HttpContext.Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                MessageError = "Vui lòng đăng nhập để thực hiện thay đổi mật khẩu";
                return Page();
            }

            // Giải mã token để lấy thông tin người dùng
            var userToken = _jwtAuthen.ParseJwtToken(token);
            if (userToken == null || userToken.UserID == Guid.Empty)
            {
                MessageError = "Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.";
                return Page();
            }

            // Khởi tạo model với UserId từ token
            Input = new ChangePassword
            {
                UserId = userToken.UserID.ToString()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostChangePassword()
        {
            if (!ModelState.IsValid)
                return Page();

            // Lấy token từ cookie
            var token = HttpContext.Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                MessageError = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.";
                return Page();
            }

            // Giải mã token để lấy thông tin người dùng
            var userToken = _jwtAuthen.ParseJwtToken(token);
            if (userToken == null || userToken.UserID == Guid.Empty)
            {
                MessageError = "Token không hợp lệ. Vui lòng đăng nhập lại.";
                return Page();
            }

            // Gán UserId từ token vào Input
            Input.UserId = userToken.UserID.ToString();

            // Gọi service để đổi mật khẩu
            var result = await _authenService.DoChangePassword(Input.UserId, Input);
            if (!result.Success)
            {
                MessageError = result.Message;
                return Page();
            }

            MessageSuccess = "Đổi mật khẩu thành công!";
            return Page();
        }
    }
}