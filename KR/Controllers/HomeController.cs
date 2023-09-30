using KR.Models;
using KR.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookStoragePortalContext _context;

        public HomeController(ILogger<HomeController> logger, BookStoragePortalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int pg = 1, Sort sortState = Sort.DateCreateAsc)
        {
            IQueryable<Book> products = _context.Books.AsQueryable();
            switch (sortState)
            {
                case Sort.DateCreateAsc: products = products.OrderBy(p => p.DateAdd); break;
                case Sort.DateCreateDesc: products = products.OrderByDescending(p => p.DateAdd); break;
            }
            const int pageSize = 2;
            if (pg < 1)
                pg = 1;
            int recsCount = products.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = await products.Skip(recSkip)
                .Take(pageSize)
                .ToListAsync();
            this.ViewBag.Pager = pager;
            ViewBag.DataCteateSort = sortState == Sort.DateCreateAsc ? Sort.DateCreateDesc : Sort.DateCreateAsc;
            return View(data);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}