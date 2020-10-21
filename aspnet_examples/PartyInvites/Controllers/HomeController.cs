using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;
using System.Linq;

namespace PartyInvites.Controllers {
    public class HomeController : Controller {
        
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
        public ViewResult RsvpForm(GuestResponse guestResponse) {
            if (ModelState.IsValid) {
                Repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            } else {
                return View();
            }
        }

        // GET /home/listresponses
        public ViewResult ListResponses() {
            //using LINQ
            return View(Repository.Responses.Where(r => r.WillAttend == true));
        }
    }
}
