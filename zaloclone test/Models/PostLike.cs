namespace zaloclone_test.Models
{
    public partial class PostLike
    {
        public string LikeId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string PostId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
