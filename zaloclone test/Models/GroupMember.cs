using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class GroupMember
    {
        public string GroupId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public bool? RoleId { get; set; }

        public virtual Groupchat Group { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
