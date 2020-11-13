using System;
using System.Collections.Generic;

#nullable disable

namespace AuctionHouse.Models
{
    public partial class Buyer
    {
        public Buyer()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
    }
}
