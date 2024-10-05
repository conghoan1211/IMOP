using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class SearchHistory
    {
        public string SearchId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string SearchedUserId { get; set; } = null!;
        public DateTime? CreateAt { get; set; }

        public virtual User SearchedUser { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
