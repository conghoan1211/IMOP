using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class CommentLike
    {
        public string LikeId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string CommentId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual Comment Comment { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
