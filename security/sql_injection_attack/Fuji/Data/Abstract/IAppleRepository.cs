using Fuji.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuji.Data.Abstract
{
    public interface IAppleRepository : IRepository<Apple>
    {
        int GetTotalConsumed(IQueryable<Apple> apples);
    }
}
