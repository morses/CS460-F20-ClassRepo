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
        //public virtual int GetTotalConsumed()
        public virtual int GetTotalConsumed(IQueryable<Apple> apples)
        {
            //IQueryable<Apple> apples = GetAll();

            // Correct version for our DB in live system using LINQ-to-SQL in default eager loading version
            // If this does fail, one solution is to enable Lazy Loading
            return apples.Include("ApplesConsumeds").Select(a => a.ApplesConsumeds.Select(ac => ac.Count).Sum()).ToList().Sum();
            // Correct version for test system which is using LINQ-to-Objects 
            //return apples.Select(a => a.ApplesConsumeds.Select(ac => ac.Count).Sum()).Sum();
        }
    }
}
