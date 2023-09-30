using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KR.Models;
using KR.Models.State;
using Microsoft.AspNetCore.Authorization;

namespace KR.Controllers
{
    
    public class BooksController : Controller
    {
        private readonly BookStoragePortalContext _context;

        public BooksController(BookStoragePortalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Books != null ? 
                          View(await _context.Books.ToListAsync()) :
                          Problem("Entity set 'BookStoragePortalContext.Books'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Create()
        {
            return View();
        }
        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,ImgUrl,YearOfRelease,Description,DateAdd,State")] Book book)
        {
            book.State = "В наличии";
            States(book);
            book.DateAdd = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(book);
        }
        public void States(Book book)
        {
            if (book.State == "В наличии")
                book.BookState = new BookInStockState();
            else
                book.BookState = new TakenBookState();
        }


        [Authorize(Roles = "User,Admin")]
        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
