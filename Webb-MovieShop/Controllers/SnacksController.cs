using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webb_MovieShop.Data;
using Webb_MovieShop.Models;

namespace Webb_MovieShop.Controllers
{
    public class SnacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SnacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Snacks
        // Hämtar snacks från sökfält
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Snack == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movie'  is null.");
            }

            var snacks = from s in _context.Snack
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                snacks = snacks.Where(s => s.Name!.Contains(searchString));
            }

            return View(await snacks.ToListAsync());
        }

        // GET: Snacks/Details/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Snack == null)
            {
                return NotFound();
            }

            var snack = await _context.Snack
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snack == null)
            {
                return NotFound();
            }

            return View(snack);
        }

        // GET: Snacks/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Snacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price")] Snack snack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(snack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(snack);
        }

        // GET: Snacks/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Snack == null)
            {
                return NotFound();
            }

            var snack = await _context.Snack.FindAsync(id);
            if (snack == null)
            {
                return NotFound();
            }
            return View(snack);
        }

        // POST: Snacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] Snack snack)
        {
            if (id != snack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(snack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SnackExists(snack.Id))
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
            return View(snack);
        }

        // GET: Snacks/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Snack == null)
            {
                return NotFound();
            }

            var snack = await _context.Snack
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snack == null)
            {
                return NotFound();
            }

            return View(snack);
        }

        // POST: Snacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Snack == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Snack'  is null.");
            }
            var snack = await _context.Snack.FindAsync(id);
            if (snack != null)
            {
                _context.Snack.Remove(snack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SnackExists(int id)
        {
          return (_context.Snack?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
