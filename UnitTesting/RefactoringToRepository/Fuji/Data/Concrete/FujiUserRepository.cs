using Fuji.Data.Abstract;
using Fuji.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuji.Data.Concrete
{
    public class FujiUserRepository : Repository<FujiUser>, IFujiUserRepository
    {
        public FujiUserRepository(FujiDbContext ctx) : base(ctx)
        {

        }

        public virtual bool Exists(FujiUser fu)
        {
            return _dbSet.Any(x => x.AspnetIdentityId == fu.AspnetIdentityId && x.FirstName == fu.FirstName && x.LastName == fu.LastName);
        }

        public virtual FujiUser GetFujiUserByIdentityId(string identityID)
        {
            return _dbSet.Where(u => u.AspnetIdentityId == identityID).FirstOrDefault();
        }

        public virtual async Task EatAsync(FujiUser user, int appleId, DateTime timestamp)
        {
            // validate inputs

            // Now we have a verified Apple and a verified User.  Let that user eat that apple!
            ApplesConsumed appleCore = new ApplesConsumed
            {
                AppleId = appleId,
                FujiUser = user,
                ConsumedAt = timestamp,
                Count = 1
            };
            _context.Add(appleCore);
            await _context.SaveChangesAsync();
            return;
        }

        public virtual Dictionary<Apple, int> GetCountOfApplesEaten(FujiUser fu)
        {
            // Not a good pattern to go through the _context to get apples.  Should be accessed
            // from outside or through the repository.  Make it a parameter
            IQueryable<Apple> apples = _context.Set<Apple>().Include("ApplesConsumeds");
            Dictionary<Apple, int> output = new Dictionary<Apple, int>();

            // Far easier way to do this than the previous version that started with the ApplesConsumed table
            foreach (Apple a in apples)
            {
                int count = a.ApplesConsumeds.Where(ac => ac.FujiUserId == fu.Id).Select(ac => ac.Count).Sum();
                output.Add(a, count);
            }

            return output;
        }

        public virtual Dictionary<Apple, int> GetCountOfSpecificApplesEaten(IEnumerable<Apple> appleList, FujiUser fu)
        {
            if (fu == null)
            {
                throw new ArgumentNullException();
            }

            // should have used our FindByIdAsync in IRepository but it's async and I'm in a hurry
            //FujiUser foundUser = _dbSet.Find(fu.Id);    // would normally need an .Include("ApplesConsumeds") here but we're assuming Lazy Loading is enabled
            // during the video: Include returns an IQueryable which doesn't have a Find method, so can't use it!
            FujiUser foundUser = _dbSet.Include("ApplesConsumeds").Where(u => u.Id == fu.Id).FirstOrDefault();

            Dictionary<Apple, int> output = new Dictionary<Apple, int>();

            if (foundUser == null || appleList == null)
            {
                return output;
            }

            foreach (Apple a in appleList)
            {
                int count = foundUser.ApplesConsumeds.Where(ac => ac.AppleId == a.Id).Select(ac => ac.Count).Sum();
                output.Add(a, count);
            }

            return output;
        }
    }
}
