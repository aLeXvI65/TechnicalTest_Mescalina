using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalTestProject_1.Models;
using TechnicalTestProject_1;
using System.Reflection;

namespace TechnicalTestProject_1.Controllers
{
    public class WinnersController : Controller
    {
        private readonly MyDbContext _context;

        public WinnersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Winners
        public async Task<IActionResult> Index()
        {
            foreach(Winner w in _context.Winner) {
                w.Name = ProtectionService.Decrypt(w.Name);
            }

            return _context.Winner != null ? 
                          View(await _context.Winner.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.Winner'  is null.");
        }

        // GET: Winners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Winner == null)
            {
                return NotFound();
            }

            var winner = await _context.Winner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (winner == null)
            {
                return NotFound();
            }

            winner.Name = ProtectionService.Decrypt(winner.Name);

            return View(winner);
        }

        // GET: Winners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Winners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,place")] Winner winner)
        {
            if (ModelState.IsValid)
            {
                winner.Name = ProtectionService.Encrypt(winner.Name);
                _context.Add(winner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(winner);
        }

        // GET: Winners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Winner == null)
            {
                return NotFound();
            }

            var winner = await _context.Winner.FindAsync(id);
            winner.Name = ProtectionService.Decrypt(winner.Name);

            if (winner == null)
            {
                return NotFound();
            }
            return View(winner);
        }

        // POST: Winners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,place")] Winner winner)
        {
            if (id != winner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    winner.Name = ProtectionService.Encrypt(winner.Name);
                    _context.Update(winner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WinnerExists(winner.Id))
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
            return View(winner);
        }

        // GET: Winners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Winner == null)
            {
                return NotFound();
            }

            var winner = await _context.Winner
                .FirstOrDefaultAsync(m => m.Id == id);

            winner.Name = ProtectionService.Decrypt(winner.Name);

            if (winner == null)
            {
                return NotFound();
            }

            return View(winner);
        }

        // POST: Winners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Winner == null)
            {
                return Problem("Entity set 'MyDbContext.Winner'  is null.");
            }
            var winner = await _context.Winner.FindAsync(id);
            if (winner != null)
            {
                _context.Winner.Remove(winner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WinnerExists(int id)
        {
          return (_context.Winner?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
