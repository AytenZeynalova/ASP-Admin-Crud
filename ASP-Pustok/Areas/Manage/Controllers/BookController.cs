using ASP_Pustok.DAL;
using ASP_Pustok.Helpers;
using ASP_Pustok.Models;
using Microsoft.AspNetCore.Hosting;
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
        private IWebHostEnvironment _env;

        public BookController(PustokDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var model = _context.Books.Include(x => x.Genre).Include(x => x.Author).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View();
        }

        [HttpPost]

        public IActionResult Create(Book book)
        {

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();


            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                return View();
            }


            if (!_context.Authors.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not exists for this authorid");
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                return View();

            }


            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre not exists for this authorid");
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                return View();

            }


            _context.Books.Add(book);
            _context.SaveChanges();

            if (book.ImageFiles != null)
            {
                foreach (var file in book.ImageFiles)
                {
                    if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFiles", "file formati image/png yaxud image/jpeg olmalidir");
                        return View();
                    }

                    if (file.Length > 2097152)
                    {
                        ModelState.AddModelError("ImageFiles", "faylin olcusu 2mb-dan artiq ola bilmez!");
                        return View();
                    }

                    if (!ModelState.IsValid)
                    {
                        ViewBag.Authors = _context.Authors.ToList();
                        ViewBag.Genres = _context.Genres.ToList();
                        return View();
                    }
                }

                foreach (var file in book.ImageFiles)
                {
                    BookImage bookImage = new BookImage
                    {

                        Name = FileManager.Save(_env.WebRootPath, "uploads/books", file),
                        Book = book
                    };
                    _context.BookImages.Add(bookImage);
                    _context.SaveChanges();
                }
            }


          

        
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Book book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return RedirectToAction("error", "dashboard");

            }

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();


            return View(book);
        }
    }
}
