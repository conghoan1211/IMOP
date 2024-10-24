namespace zaloclone_test.Models
{
    public partial class MessageReaction
    {
        public string MessageId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? Reaction { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Message Message { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
