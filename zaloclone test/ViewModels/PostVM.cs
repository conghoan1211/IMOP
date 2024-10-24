using System.ComponentModel.DataAnnotations;
using zaloclone_test.Helper;

namespace zaloclone_test.ViewModels
{
    public class PostVM
    {
        public string? PostID { get; set; }
        public string? UserID { get; set; }
        public string? Username { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Content { get; set; }
        public int? Privacy { get; set; }
        public int? Likes { get; set; }
        public int? Shares { get; set; } = 0;
        public int? Comments { get; set; }
        public bool? IsComment { get; set; } = true;
        public int? Views { get; set; }
        public bool IsLikePost { get; set; }
        public bool? PinToTop { get; set; } = false;
        public DateTime? CreateAt { get; set; }
        public string? Images { get; set; }
    }

    public class PostImageVM
    {
        public Guid ImageId { get; set; }
        public Guid PostId { get; set; }
        [Required]
        public IFormFile[]? Images { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }

    public class PostLikeVM
    {
        public Guid LikeId { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public DateTime? CreatedAt { get; set; }

    }

    public class InsertUpdatePostVM
    {
        public string PostID { get; set; } = string.Empty;
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "Content is required")]
        [MaxLength(500, ErrorMessage = "Content not exceeding 500 characters.")]
        public string Content { get; set; } = string.Empty;
        public int Privacy { get; set; } = (int)PostPrivacy.Public;
        public bool IsComment { get; set; } = true;
        public IFormFile[]? Images { get; set; } = null!;
        public bool? PinToTop { get; set; } = false;
    }


    public class PostCommentVM
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public int? Likes { get; set; }
        public int? IsHide { get; set; }
        public bool? IsPinTop { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CommentImageVM
    {
        public Guid ImageId { get; set; }
        public Guid CommentId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }

    public class CommentLikeVM
    {
        public Guid LikeId { get; set; }
        public Guid UserId { get; set; }
        public string CommentId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }

    public class PostSearchVM
    {
        public string UserID { get; set; }
        public int? Privacy { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
