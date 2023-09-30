using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KR.Models;
using Microsoft.AspNetCore.Authorization;
using KR.Models.State;

namespace KR.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class UsersController : Controller
    {
        private readonly BookStoragePortalContext _context;

        public UsersController(BookStoragePortalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int Id)
        {
            User user = await _context.Users.FirstOrDefaultAsync();

            if (user != null)
            {
                Book selectedBook = await _context.Books.FindAsync(Id);

                    if (user.Books == null)
                    {
                        user.Books = new List<Book>();
                    }
                    States(selectedBook);
                    user.Books.Add(selectedBook);
                    await _context.SaveChangesAsync();

                return View(user);
            }
            else
            {
                return Problem("No users found.");
            }
        }
        public void States(Book book)
        {
            if (book.State == "В наличии")
                book.BookState = new BookInStockState();
            else
                book.BookState = new TakenBookState();
        }

        public async Task<IActionResult> LCabinet(int Id)
        {
            User user = await _context.Users.FirstOrDefaultAsync();

            if (user != null)
            {
                Book selectedBook = await _context.Books.FindAsync(Id);

                if (selectedBook != null)
                {
                    if (user.Books == null)
                    {
                        user.Books = new List<Book>();
                    }
                    States(selectedBook);
                    user.Books.Add(selectedBook);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Problem("Selected book not found.");
                }
                return View(user); 
            }
            else
            {
                return Problem("No user found.");
            }
        }

        public async Task<IActionResult> LCabinets()
        {

            User user = await _context.Users.FirstOrDefaultAsync();

            if (user != null)
            {

                return View(user);
            }
            else
            {
                return Problem("No user found.");
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,ContactPhone")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        private bool WorkerExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}

