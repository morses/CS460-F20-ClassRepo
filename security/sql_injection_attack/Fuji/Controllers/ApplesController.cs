using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fuji.Data;
using Fuji.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Fuji.Data.Abstract;

namespace Fuji.Controllers
{
    public class ApplesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IFujiUserRepository _fuRepo;
        private readonly IAppleRepository _appleRepo;

        public ApplesController(UserManager<IdentityUser> userManager, IFujiUserRepository fuRepo, IAppleRepository appleRepo)
        {
            _userManager = userManager;
            _fuRepo = fuRepo;
            _appleRepo = appleRepo;
        }

        // AJAX endpoint for eating an apple
        [HttpPost]
        [Authorize]
        // Note: can be forged as we don't have an anti forgery token.  Will need to add this
        public async Task<JsonResult> Ate(int? id)
        {
            // verify id is actually a real apple
            if (id == null)
            {
                return Json(new { success = false, message = "id expected" });
            }
            //if (!AppleExists((int)id))
            if(!await _appleRepo.ExistsAsync((int)id))
            {
                return Json(new { success = false, message = "appleID not found" });
            }

            // and that we have a logged in user
            string aspNetUserID = _userManager.GetUserId(User);
            if(aspNetUserID == null)
            {
                return Json(new { success = false, message = "user not logged in" });
            }
            FujiUser fu = null;
            if (aspNetUserID != null)
            {
                //fu = _context.FujiUsers.Where(u => u.AspnetIdentityId == aspNetUserID).FirstOrDefault();
                fu = _fuRepo.GetFujiUserByIdentityId(aspNetUserID);
                if (fu == null)
                {
                    return Json(new { success = false, message = "user not found" });
                }
            }

            // Now we have a verified Apple and a verified User.  Let that user eat that apple!
            /*ApplesConsumed appleCore = new ApplesConsumed {
                Apple = await _context.Apples.FirstOrDefaultAsync(a => a.Id == id),
                FujiUser = fu,
                ConsumedAt = DateTime.UtcNow,
                Count = 1
            };
            _context.Add(appleCore);
            await _context.SaveChangesAsync();
            */
            await _fuRepo.EatAsync(fu, (int)id, DateTime.UtcNow);
            return Json(new { success = true, message = "user ate apple" });
        }

        // AJAX endpoint
        // GET: Apples eaten by this user; could also filter by apples eaten today or yesterday, etc.
        [Authorize]
        public JsonResult Eaten()
        {
            // Find the current user
            string aspNetUserID = _userManager.GetUserId(User);
            if (aspNetUserID == null)
            {
                return Json(new { success = false, message = "user not logged in" });
            }
            FujiUser fu = null;
            if (aspNetUserID != null)
            {
                //fu = _context.FujiUsers.Where(u => u.AspnetIdentityId == aspNetUserID).FirstOrDefault();
                fu = _fuRepo.GetFujiUserByIdentityId(aspNetUserID);
                if (fu == null)
                {
                    return Json(new { success = false, message = "user not found" });
                }
            }
            // We need to use var here since we're selecting a list of a dynamic object type (by using select new {}).  This is a complex LINQ query.  Use LinqPad to break it down and see what each step does.  I'm not sure it needs both select statements but it was easiest that way.  A join, groupby and then select would have worked.
            /*
            var apples = _context.ApplesConsumeds
                .Where(ac => ac.FujiUser == fu)
                .Select(ac => new
                {
                    VarietyName = ac.Apple.VarietyName,
                    Count = ac.Count
                })           
                .GroupBy(ac => ac.VarietyName)
                .Select(g => new
                {
                    VarietyName = g.Key,
                    Total = g.Sum(x => x.Count)
                });
            */
            // refactored to ...
            Dictionary<Apple, int> values = _fuRepo.GetCountOfSpecificApplesEaten(_appleRepo.GetAll(),fu);
            //Total and VarietyName
            var apples = values.Select(v => new { VarietyName = v.Key.VarietyName, Total = v.Value });
            return Json(new { success = true, message = "ok", apples = apples });
        }

        /*private bool AppleExists(int id)
        {
            return _context.Apples.Any(e => e.Id == id);
        }*/

    }
}
