 using System.ComponentModel.DataAnnotations;

    namespace zaloclone_test.ViewModels
    {
        public class AsideContactVM
        {
            public string? UserId { get; set; }
            public string? Username { get; set; }
            public string? Avatar { get; set; }
            public bool IsBlocked { get; set; }
            public DateTime? LastInteraction { get; set; }
            public bool IsOnline { get; set; }
        }

        public class FriendFilterModel
        {
            public string? SearchTerm { get; set; }
            public string? SortOrder { get; set; } // "name_asc", "name_desc", "date_asc", "date_desc"
            public string? FilterType { get; set; } // "all", "online", "blocked"
        }

        public class BlockFriendModel
        {
            [Required(ErrorMessage = "UserId không được để trống")]
            public string? UserId { get; set; }

            [Required(ErrorMessage = "Reason không được để trống")]
            [StringLength(200, ErrorMessage = "Lý do chặn không được vượt quá 200 ký tự")]
            public string? Reason { get; set; }
        }

        public class FriendProfileModel
        {
            public string? UserId { get; set; }
            public string? Username { get; set; }
            public string? Avatar { get; set; }
            public string? Bio { get; set; }
            public DateTime? Dob { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public bool IsVerified { get; set; }
            public DateTime? FriendsSince { get; set; }
            public int MutualFriends { get; set; }
        }

        public class FriendOperationResult
        {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public object? Data { get; set; }
        }
    }
