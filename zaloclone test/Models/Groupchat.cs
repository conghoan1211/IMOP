using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class Groupchat
    {
        public Groupchat()
        {
            GroupMembers = new HashSet<GroupMember>();
            Messages = new HashSet<Message>();
        }

        public string GroupId { get; set; } = null!;
        public string? GroupName { get; set; }
        public int? Type { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }

        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
