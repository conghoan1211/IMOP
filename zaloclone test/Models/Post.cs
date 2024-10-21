using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class Post
    {
        public Post()
        {
            CommentsNavigation = new HashSet<Comment>();
            PostImages = new HashSet<PostImage>();
            PostLikes = new HashSet<PostLike>();
        }

        public string PostId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public int? Privacy { get; set; }
        public int? Likes { get; set; }
        public int? Comments { get; set; }
        public int? Shares { get; set; }
        public int? Views { get; set; }
        public bool? IsComment { get; set; }
        public bool? PinTop { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> CommentsNavigation { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
    }
}
