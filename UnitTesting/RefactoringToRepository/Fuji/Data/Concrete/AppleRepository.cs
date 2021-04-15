using Fuji.Data.Abstract;
using Fuji.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuji.Data.Concrete
{
    public class AppleRepository : Repository<Apple>, IAppleRepository
    {
        public AppleRepository(FujiDbContext ctx) : base(ctx)
        {

        }

        // Should be incorrect right now, which should show up with a test
        public virtual int GetTotalConsumed()
        {
            IQueryable<Apple> apples = GetAll();
            return apples.Select(a => a.ApplesConsumeds.Select(ac => ac.Count).Sum()).Sum();
        }
    }
}
