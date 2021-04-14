using System;
using System.Collections.Generic;

#nullable disable

namespace Fuji.Models
{
    public partial class Apple
    {
        public Apple()
        {
            ApplesConsumeds = new HashSet<ApplesConsumed>();
        }

        public int Id { get; set; }
        public string VarietyName { get; set; }
        public string ScientificName { get; set; }

        public virtual ICollection<ApplesConsumed> ApplesConsumeds { get; set; }
    }
}
