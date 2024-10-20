using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class Comment
    {
        public Comment()
        {
            CommentImages = new HashSet<CommentImage>();
            CommentLikes = new HashSet<CommentLike>();
        }

        public string CommentId { get; set; } = null!;
        public string PostId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int? Likes { get; set; }
        public int? IsHide { get; set; }
        public bool? IsPinTop { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<CommentImage> CommentImages { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
    }
}
