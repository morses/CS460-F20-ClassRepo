using System;
using System.Collections.Generic;

#nullable disable

namespace AuctionHouse.Models
{
    public partial class Seller
    {
        public Seller()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
