using System;
using System.Collections.Generic;

#nullable disable

namespace Fuji.Models
{
    public partial class ApplesConsumed
    {
        public int Id { get; set; }
        public int FujiUserId { get; set; }
        public int AppleId { get; set; }
        public int Count { get; set; }
        public DateTime ConsumedAt { get; set; }

        public virtual Apple Apple { get; set; }
        public virtual FujiUser FujiUser { get; set; }
    }
}
