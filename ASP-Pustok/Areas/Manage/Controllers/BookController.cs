using ASP_Pustok.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.Areas.Manage.Controllers
{

    [Area("manage")]
    public class BookController : Controller
    {

        private PustokDbContext _context;

        public BookController(PustokDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Books.Include(x => x.Genre).Include(x => x.Author).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
