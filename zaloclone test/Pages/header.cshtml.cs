using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zaloclone_test.Services;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Pages
{
    public class Index1Model : PageModel
    {
        private readonly IProfile _profileService;

        // Thêm thu?c tính ?? l?u tr? thông tin h? s?
        public ProfileViewModels Profile { get; set; }

        public Index1Model(IProfile profileService)
        {
            _profileService = profileService;
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            // Ki?m tra userId có h?p l? không
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId không h?p l?.");
            }

            // T?i thông tin h? s? t? d?ch v?
            Profile = await _profileService.ViewProfile(userId);
            if (Profile == null)
            {
                // X? lý n?u không tìm th?y h? s?
                return NotFound(); // Ho?c có th? redirect ??n m?t trang khác
            }

            return Page();
        }


        public async Task<IActionResult> OnPostUpdateProfileAsync(UpdateProfileModels updateProfile)
        {
            var result = await _profileService.UpdateProfile(updateProfile);
            if (result.Success)
            {
                // Hi?n th? thông báo thành công
                TempData["SuccessMessage"] = result.Message;
                return RedirectToPage();
            }
            else
            {
                // Hi?n th? thông báo l?i
                ModelState.AddModelError(string.Empty, result.Message);
            }

            // Tr? v? trang v?i l?i xác th?c
            return Page();
        }
    }
}