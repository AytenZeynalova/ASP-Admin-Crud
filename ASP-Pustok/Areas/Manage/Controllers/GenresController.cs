using ASP_Pustok.DAL;
using ASP_Pustok.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class GenresController : Controller
    {
        private PustokDbContext _context;

        public GenresController(PustokDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            var genres = _context.Genres.Include(x => x.Books).ToList();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]

        public IActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Genres.Add(genre);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            Genre existAuth = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);
            existAuth.Name = genre.Name;
           
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Delete(int id)
        {
            Genre genre = _context.Genres.Include(x => x.Books).FirstOrDefault(x => x.Id == id);
            if (genre == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(genre);
        }

        [HttpPost]
        public IActionResult Delete(Genre genre)
        {
            Genre existAuth = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);
            if (existAuth == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            _context.Genres.Remove(existAuth);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult SweetDelete(int id)
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return Ok();

        }

    }
}
