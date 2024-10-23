using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class MessageBlock
    {
        public MessageBlock()
        {
            Messages = new HashSet<Message>();
        }

        public string MessageBlockId { get; set; } = null!;
        public DateTime? FirstSendDate { get; set; }
        public string ConversationId { get; set; } = null!;

        public virtual Conversation Conversation { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
    }
}
