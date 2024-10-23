using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class Friend
    {
        public string FriendId { get; set; } = null!;
        public string? UserId1 { get; set; }
        public string? UserId2 { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }

        public virtual User? UserId1Navigation { get; set; }
        public virtual User? UserId2Navigation { get; set; }
    }
}
