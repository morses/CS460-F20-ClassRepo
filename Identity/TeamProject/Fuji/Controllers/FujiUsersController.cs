using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fuji.Data;
using Fuji.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fuji.Controllers
{
    [Authorize(Roles = "admin")]
    public class FujiUsersController : Controller
    {
        private readonly FujiDbContext _context;

        public FujiUsersController(FujiDbContext context)
        {
            _context = context;
        }

        // GET: FujiUsers
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.FujiUsers.ToListAsync());
        }

        // GET: FujiUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fujiUser = await _context.FujiUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fujiUser == null)
            {
                return NotFound();
            }

            return View(fujiUser);
        }

        // GET: FujiUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FujiUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AspnetIdentityId,FirstName,LastName")] FujiUser fujiUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fujiUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fujiUser);
        }

        // GET: FujiUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fujiUser = await _context.FujiUsers.FindAsync(id);
            if (fujiUser == null)
            {
                return NotFound();
            }
            return View(fujiUser);
        }

        // POST: FujiUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AspnetIdentityId,FirstName,LastName")] FujiUser fujiUser)
        {
            if (id != fujiUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fujiUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FujiUserExists(fujiUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fujiUser);
        }

        // GET: FujiUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fujiUser = await _context.FujiUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fujiUser == null)
            {
                return NotFound();
            }

            return View(fujiUser);
        }

        // POST: FujiUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fujiUser = await _context.FujiUsers.FindAsync(id);
            _context.FujiUsers.Remove(fujiUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FujiUserExists(int id)
        {
            return _context.FujiUsers.Any(e => e.Id == id);
        }
    }
}
