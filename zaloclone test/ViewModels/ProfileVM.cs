﻿using ExpressiveAnnotations.Attributes;
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

    public class UpdateAvatarVM
    {
        [AssertThat("Image.Length <= MaxFileSize", ErrorMessage = "File size must not exceed {MaxFileSize} bytes")]
        public IFormFile? Image { get; set; }
        public long MaxFileSize => 1048576;

    }

    public class UpdateProfileModels
    {
        [Required(ErrorMessage = "Tên hiển thị không được để trống")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Giới tính không được để trống")]
        [Range(0, 2, ErrorMessage = "Giới tính không hợp lệ. Vui lòng chọn: 0 (Nam), 1 (Nữ), hoặc 2 (Khác)")]
        public int? Sex { get; set; }
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        //[BirthYearValidation(1890)]
        [AssertThat("Dob <= Now()", ErrorMessage = "Ngày sinh không vượt quá ngày hiện tại!")]
        public DateTime? Dob { get; set; }
        [StringLength(50, ErrorMessage = "Bio không được vượt quá 50 ký tự")]
        public string? Bio { get; set; }
    }

    public class BirthYearValidationAttribute : ValidationAttribute
    {
        private readonly int _minYear;
        public BirthYearValidationAttribute(int minYear)
        {
            _minYear = minYear;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var date = (DateTime)value;

            if (date.Year < _minYear)
                return new ValidationResult($"Năm sinh phải lớn hơn hoặc bằng {_minYear}!");

            return ValidationResult.Success;
        }
    }
}