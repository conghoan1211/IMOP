﻿namespace zaloclone_test.Models
{
    public partial class Message
    {
        public Message()
        {
            MessageReactions = new HashSet<MessageReaction>();
            MessageStatuses = new HashSet<MessageStatus>();
        }

        public string MessageId { get; set; } = null!;
        public string SenderId { get; set; } = null!;
        public string MessageBlockId { get; set; } = null!;
        public string? Content { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsHide { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? File { get; set; }

        public virtual MessageBlock MessageBlock { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
        public virtual ICollection<MessageStatus> MessageStatuses { get; set; }
    }
}
