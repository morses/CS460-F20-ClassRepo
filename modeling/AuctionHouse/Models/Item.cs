using System;
using System.Collections.Generic;

#nullable disable

namespace AuctionHouse.Models
{
    public partial class Item
    {
        public Item()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SellerId { get; set; }

        public virtual Seller Seller { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
