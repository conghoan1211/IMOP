using System;
using System.Collections.Generic;

namespace zaloclone_test.Models
{
    public partial class MessageStatus
    {
        public string StatusId { get; set; } = null!;
        public string? MessageId { get; set; }
        public string? UserId { get; set; }
        public int? Status { get; set; }

        public virtual Message? Message { get; set; }
        public virtual User? User { get; set; }
    }
}
