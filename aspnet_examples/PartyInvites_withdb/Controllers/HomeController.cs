using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;
using System.Linq;

namespace PartyInvites.Controllers {
    public class HomeController : Controller {
        
        private PartyInvitesDbContext db;

        public HomeController(PartyInvitesDbContext db)
        {
            this.db = db;
        }

        // GET /Home/Index   GET /home/index
        public IActionResult Index() {
            return View();
        }

        // GET /Home/RsvpForm
        [HttpGet]
        public ViewResult RsvpForm() {
            return View();
        }

        // POST /home/rsvpform
        [HttpPost]
        public ViewResult RsvpForm(GuestResponses guestResponse) {
            if (ModelState.IsValid) {
                //Repository.AddResponse(guestResponse);
                db.GuestResponses.Add(guestResponse);
                db.SaveChanges();
                return View("Thanks", guestResponse);
            } else {
                return View();
            }
        }

        // GET /home/listresponses
        public ViewResult ListResponses() {
            //using LINQ
            return View(db.GuestResponses.Where(r => r.WillAttend == true));
            //return View(Repository.Responses.Where(r => r.WillAttend == true));
        }
    }
}
