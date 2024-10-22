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
}
