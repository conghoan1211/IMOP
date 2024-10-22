using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;
using zaloclone_test.Models;

namespace zaloclone_test.ViewModels
{
    public class ProfileVM
    {
        public string? UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Avatar { get; set; } = string.Empty;
        public int? RoleID { get; set; }
        public string? Bio {  get; set; } = string.Empty;
        public int? Sex { get; set; }
        public DateTime? Dob {  get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }              /* Admin choosen*/
        public bool? IsDisable { get; set; }                /*User choosen*/
        public int? Status { get; set; }                       /* trang thai hoat dong*/
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public int? NumberOfFriends { get; set; } = 0;
    }

    public class UpdateProfileModels
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Giới tính không được để trống")]
        [Range(1, 3, ErrorMessage = "Giới tính không hợp lệ. Vui lòng chọn: 1 (Nam), 2 (Nữ), hoặc 3 (Khác)")]
        public int Sex { get; set; }
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        [AssertThat("Dob <= Now()", ErrorMessage = "Ngày sinh không vượt quá ngày hiện tại!")]
        [AssertThat("Dob.Year >= 1890", ErrorMessage = "Ngày sinh không hợp lệ!")]
        public DateTime Dob { get; set; }
        [StringLength(60, ErrorMessage = "Bio không được vượt quá 60 ký tự")]
        public string Bio { get; set; }
    }
}
