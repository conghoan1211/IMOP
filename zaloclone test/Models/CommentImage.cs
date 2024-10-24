namespace zaloclone_test.Models
{
    public partial class CommentImage
    {
        public string ImageId { get; set; } = null!;
        public string CommentId { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual Comment Comment { get; set; } = null!;
    }
}
