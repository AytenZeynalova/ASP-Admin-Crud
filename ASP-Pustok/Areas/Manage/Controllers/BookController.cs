using ASP_Pustok.DAL;
using ASP_Pustok.Helpers;
using ASP_Pustok.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            if (!_context.Authors.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not exists for this authorid");

            }


            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre not exists for this authorid");

            }

            CheckCreatePosterFile(book);
            CheckImageFiles(book);
            CheckCreateHoverPosterFile(book);

            if (!ModelState.IsValid)
            {
                ViewBag.Genres = _context.Genres.ToList();
                ViewBag.Authors = _context.Authors.ToList();

                return View();
            }



            BookImage bookPosterImage = new BookImage
            {

                Name = FileManager.Save(_env.WebRootPath, "uploads/books", book.PosterFile),
                PosterStatus = true
            };



            BookImage bookHoverPosterFile = new BookImage
            {

                Name = FileManager.Save(_env.WebRootPath, "uploads/books", book.HoverPosterFiler),
                PosterStatus = false
            };
            book.BookImages.Add(bookPosterImage);
            book.BookImages.Add(bookHoverPosterFile);
            AddImageFiles(book,book.ImageFiles);


            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        private void CheckImageFiles(Book book)

        {

            if (book.ImageFiles != null)
            {
                foreach (var file in book.ImageFiles)
                {
                    if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFiles", "file formati image/png yaxud image/jpeg olmalidir");
                    }

                    if (file.Length > 2097152)
                    {
                        ModelState.AddModelError("ImageFiles", "faylin olcusu 2mb-dan artiq ola bilmez!");
                    }


                }

            }
        }

        private void CheckCreateHoverPosterFile(Book book)
        {

            if (book.HoverPosterFiler == null)
            {
                ModelState.AddModelError("HoverPosterFiler", "HoverPosterFiler File is Required");

            }
            else
            {
                CheckHoverPosterFile(book);
            }
        }
        private void CheckHoverPosterFile(Book book)
        {
                if (book.HoverPosterFiler.ContentType != "image/png" && book.HoverPosterFiler.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterFile", "file formati image/png yaxud image/jpeg olmalidir");
                }

                if (book.HoverPosterFiler.Length > 2097152)
                {
                    ModelState.AddModelError("HoverPosterFiler", "faylin olcusu 2mb-dan artiq ola bilmez!");
                }
            
        }

        private void CheckCreatePosterFile(Book book)
        {
            if (book.PosterFile == null)
            {
                ModelState.AddModelError("PosterFile", "Poster File is Required");

            }
            else
            {
                CheckCreatePosterFile(book);
            }
        }
        private void CheckPosterFiles(Book book)
        {
                if (book.PosterFile.ContentType != "image/png" && book.PosterFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterFile", "file formati image/png yaxud image/jpeg olmalidir");
                }

                if (book.PosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterFile", "faylin olcusu 2mb-dan artiq ola bilmez!");
                }
            
        }

        private void AddImageFiles(Book book , List<IFormFile>images)
        {
            if (images != null)
            {

                foreach (var file in images)
                {
                    BookImage bookImage = new BookImage
                    {
                        Name = FileManager.Save(_env.WebRootPath, "uploads/books", file),
                        PosterStatus = null,
                    };
                    book.BookImages.Add(bookImage);
                    _context.SaveChanges();
                }
            }
        }
        public IActionResult Edit(int id)
        {
            Book book = _context.Books.Include(x=>x.BookImages).FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {

            if (!_context.Authors.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not exists for this authorid");

            }


            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre not exists for this authorid");
            }


            if(book.PosterFile!=null)
            CheckPosterFiles(book);

            CheckImageFiles(book);

            

            if (!ModelState.IsValid)
            {
                ViewBag.Genres = _context.Genres.ToList();
                ViewBag.Authors = _context.Authors.ToList();

                return View();
            }



            Book existbook = _context.Books.Include(x=>x.BookImages).FirstOrDefault(x => x.Id == book.Id);
            if (existbook == null)
            return RedirectToAction("error", "dashboard");

            List<string> deletedFiles = new List<string>();



            if (book.PosterFile != null)
            {

                BookImage poster = existbook.BookImages.FirstOrDefault(x => x.PosterStatus == true);
                deletedFiles.Add(poster.Name);

                poster.Name = FileManager.Save(_env.WebRootPath, "uploads/book", book.PosterFile);
            }

            AddImageFiles(existbook, book.ImageFiles);


                existbook.Rate = book.Rate;
                existbook.Name = book.Name;
                existbook.IsAvailable = book.IsAvailable;
                existbook.PageSize = book.PageSize;
                existbook.SubDesc = book.SubDesc;
                existbook.Desc = book.Desc;
                existbook.GenreId = book.GenreId;
                existbook.AuthorId = book.AuthorId;
                existbook.CostPrice = book.CostPrice;
                existbook.SalePrice = book.SalePrice;
                existbook.DiscountPercent = book.DiscountPercent;
                _context.SaveChanges();
                return RedirectToAction("index");
                     

        }

    }
}
