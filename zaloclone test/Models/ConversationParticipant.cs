﻿using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class ConversationParticipant
    {
        public string ConversationId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public bool? RoleId { get; set; }

        public virtual Conversation Conversation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
