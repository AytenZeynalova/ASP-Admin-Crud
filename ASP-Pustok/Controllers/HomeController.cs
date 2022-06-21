using ASP_Pustok.DAL;
using ASP_Pustok.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.Controllers
{
    public class HomeController : Controller
    {
        private PustokDbContext _context;

        public HomeController(PustokDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                HomeSliderss = _context.HomeSliders.ToList(),
                HomeFuturess = _context.HomeFutures.ToList(),
                DiscountedBooks = _context.Books.Include(x => x.BookImages).Include(x=>x.Author).ToList()
                .Where(x=>x.DiscountPercent>0).Take(20).ToList(),

                FeaturedBooks= _context.Books.Include(x => x.BookImages).Include(x => x.Author).ToList()
                .Where(x => x.IsFeatured).Take(20).ToList(),

                NewBooks= _context.Books.Include(x => x.BookImages).Include(x => x.Author).ToList()
                .Where(x => x.InNew).Take(20).ToList(),
            };

            return View(homeViewModel);
        }
    }
}
