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
    public class AuthorController : Controller
    {
        private readonly PustokDbContext _context;

        public AuthorController(PustokDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Authors.Include(x => x.Books).ToList();
            return View(data);
        }
            
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            Author author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if(author== null)
            {
               return RedirectToAction("error", "dashboard");
            }
            return View(author);
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            Author existAuth = _context.Authors.FirstOrDefault(x => x.Id == author.Id);
            existAuth.FullName = author.FullName;
            existAuth.BirthDate = author.BirthDate;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Author author = _context.Authors.Include(x=> x.Books).FirstOrDefault(x => x.Id == id);
            if(author== null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(author);
        }

        [HttpPost]
        public IActionResult Delete(Author author)
        {
            Author existAuth = _context.Authors.FirstOrDefault(x => x.Id == author.Id);
            if (existAuth == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            _context.Authors.Remove(existAuth);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
