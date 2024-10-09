using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;
namespace zaloclone_test.ViewModels
{
    public class ProfileViewModels
    {
        public string UserId { get; set; }
        public string AvatarUrl { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public int Sex { get; set; }
        public string Bio { get; set; }
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