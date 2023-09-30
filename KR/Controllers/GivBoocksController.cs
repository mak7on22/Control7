using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KR.Models;

namespace KR.Controllers
{
    public class GivBoocksController : Controller
    {
        private readonly BookStoragePortalContext _context;

        public GivBoocksController(BookStoragePortalContext context)
        {
            _context = context;
        }

        // GET: GivBoocks
        public async Task<IActionResult> Index(int Id)
        {
            var bookStoragePortalContext = _context.GivBoocks.Include(g => g.GivBook).Include(g => g.GivUser);
            return View(await bookStoragePortalContext.ToListAsync());
        }

        // GET: GivBoocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GivBoocks == null)
            {
                return NotFound();
            }

            var givBoock = await _context.GivBoocks
                .Include(g => g.GivBook)
                .Include(g => g.GivUser)
                .FirstOrDefaultAsync(m => m.GivBoockId == id);
            if (givBoock == null)
            {
                return NotFound();
            }

            return View(givBoock);
        }

        // GET: GivBoocks/Create
        public IActionResult Create()
        {
            ViewData["GivBookId"] = new SelectList(_context.Books, "Id", "Author");
            ViewData["GivUserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: GivBoocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GivBoockId,GivUserId,GivBookId,DateGiv,GivDateReturn")] GivBoock givBoock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(givBoock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GivBookId"] = new SelectList(_context.Books, "Id", "Author", givBoock.GivBookId);
            ViewData["GivUserId"] = new SelectList(_context.Users, "UserId", "UserId", givBoock.GivUserId);
            return View(givBoock);
        }

        // GET: GivBoocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GivBoocks == null)
            {
                return NotFound();
            }

            var givBoock = await _context.GivBoocks.FindAsync(id);
            if (givBoock == null)
            {
                return NotFound();
            }
            ViewData["GivBookId"] = new SelectList(_context.Books, "Id", "Author", givBoock.GivBookId);
            ViewData["GivUserId"] = new SelectList(_context.Users, "UserId", "UserId", givBoock.GivUserId);
            return View(givBoock);
        }

        // POST: GivBoocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GivBoockId,GivUserId,GivBookId,DateGiv,GivDateReturn")] GivBoock givBoock)
        {
            if (id != givBoock.GivBoockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(givBoock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GivBoockExists(givBoock.GivBoockId))
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
            ViewData["GivBookId"] = new SelectList(_context.Books, "Id", "Author", givBoock.GivBookId);
            ViewData["GivUserId"] = new SelectList(_context.Users, "UserId", "UserId", givBoock.GivUserId);
            return View(givBoock);
        }

        // GET: GivBoocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GivBoocks == null)
            {
                return NotFound();
            }

            var givBoock = await _context.GivBoocks
                .Include(g => g.GivBook)
                .Include(g => g.GivUser)
                .FirstOrDefaultAsync(m => m.GivBoockId == id);
            if (givBoock == null)
            {
                return NotFound();
            }

            return View(givBoock);
        }

        // POST: GivBoocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GivBoocks == null)
            {
                return Problem("Entity set 'BookStoragePortalContext.GivBoocks'  is null.");
            }
            var givBoock = await _context.GivBoocks.FindAsync(id);
            if (givBoock != null)
            {
                _context.GivBoocks.Remove(givBoock);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GivBoockExists(int id)
        {
          return (_context.GivBoocks?.Any(e => e.GivBoockId == id)).GetValueOrDefault();
        }
    }
}
