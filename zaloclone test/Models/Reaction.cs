using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class Reaction
    {
        public Reaction()
        {
            MessageReactions = new HashSet<MessageReaction>();
        }

        public int ReactionId { get; set; }
        public string ReactionName { get; set; } = null!;

        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
    }
}
