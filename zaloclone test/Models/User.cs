using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class User
    {
        public User()
        {
            CommentLikes = new HashSet<CommentLike>();
            Comments = new HashSet<Comment>();
            FriendUserId1Navigations = new HashSet<Friend>();
            FriendUserId2Navigations = new HashSet<Friend>();
            GroupMembers = new HashSet<GroupMember>();
            MessageReactions = new HashSet<MessageReaction>();
            MessageStatuses = new HashSet<MessageStatus>();
            Messages = new HashSet<Message>();
            PostLikes = new HashSet<PostLike>();
            Posts = new HashSet<Post>();
            SearchHistorySearchedUsers = new HashSet<SearchHistory>();
            SearchHistoryUsers = new HashSet<SearchHistory>();
        }

        public string UserId { get; set; } = null!;
        public string? Username { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public int? RoleId { get; set; }
        public int? Sex { get; set; }
        public DateTime? Dob { get; set; }
        public string? Bio { get; set; }
        public bool? IsDisable { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Friend> FriendUserId1Navigations { get; set; }
        public virtual ICollection<Friend> FriendUserId2Navigations { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
        public virtual ICollection<MessageStatus> MessageStatuses { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<SearchHistory> SearchHistorySearchedUsers { get; set; }
        public virtual ICollection<SearchHistory> SearchHistoryUsers { get; set; }
    }
}
