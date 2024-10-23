using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class Conversation
    {
        public Conversation()
        {
            ConversationParticipants = new HashSet<ConversationParticipant>();
            MessageBlocks = new HashSet<MessageBlock>();
        }

        public string ConversationId { get; set; } = null!;
        public string? ConversationName { get; set; }
        public int Type { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }

        public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; }
        public virtual ICollection<MessageBlock> MessageBlocks { get; set; }
    }
}
