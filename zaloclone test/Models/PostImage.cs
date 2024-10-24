namespace zaloclone_test.Models
{
    public partial class PostImage
    {
        public string ImageId { get; set; } = null!;
        public string PostId { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
