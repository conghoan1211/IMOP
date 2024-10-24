namespace zaloclone_test.Models
{
    public partial class GroupMember
    {
        public string GroupMemberId { get; set; } = null!;
        public string? GroupId { get; set; }
        public string? UserId { get; set; }
        public bool? RoleId { get; set; }

        public virtual Groupchat? Group { get; set; }
        public virtual User? User { get; set; }
    }
}
