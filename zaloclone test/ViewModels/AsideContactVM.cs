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

    // Simplified block model without reason
    public class BlockFriendModel
    {
        public string? UserId { get; set; }
    }

    public class FriendOperationResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}

