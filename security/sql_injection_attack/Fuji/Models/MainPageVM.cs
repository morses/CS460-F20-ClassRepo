using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuji.Models
{
    public class MainPageVM
    {
        public IdentityUser TheIdentityUser { get; set; }
        public FujiUser TheFujiUser { get; set; }
        public IEnumerable<Apple> Apples { get; set; }
    }
}
