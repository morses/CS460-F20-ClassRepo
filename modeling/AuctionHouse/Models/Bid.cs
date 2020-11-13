using System;
using System.Collections.Generic;

#nullable disable

namespace AuctionHouse.Models
{
    public partial class Bid
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public DateTime? TimeSubmitted { get; set; }
        public int? BuyerId { get; set; }
        public int? ItemId { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual Item Item { get; set; }
    }
}
