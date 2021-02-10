using System;
using System.Collections.Generic;

#nullable disable

namespace Fuji.Models
{
    public partial class FujiUser
    {
        public FujiUser()
        {
            ApplesConsumeds = new HashSet<ApplesConsumed>();
        }

        public int Id { get; set; }
        public string AspnetIdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ApplesConsumed> ApplesConsumeds { get; set; }

    }
}
