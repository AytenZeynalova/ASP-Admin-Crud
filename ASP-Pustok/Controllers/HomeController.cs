using ASP_Pustok.DAL;
using ASP_Pustok.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
                HomeFuturess=_context.HomeFutures.ToList(),
            };

            return View(homeViewModel);
        }
    }
}
