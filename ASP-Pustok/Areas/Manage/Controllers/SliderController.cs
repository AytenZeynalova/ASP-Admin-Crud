using ASP_Pustok.DAL;
using ASP_Pustok.Helpers;
using ASP_Pustok.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly IWebHostEnvironment _enviro;

        public SliderController(PustokDbContext context , IWebHostEnvironment enviro)
        {
            _context = context;
            _enviro = enviro;
        }
        public IActionResult Index()
        {
            var model = _context.HomeSliders.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HomeSlider slider)
        {
            if (!ModelState.IsValid)
                return View();

            if(slider.ImageFiles.ContentType!="image/png" && slider.ImageFiles.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFiles", "file formati image/png yaxud image/jpeg olmalidir");
                return View();
            }

            if(slider.ImageFiles.Length> 2097152)
            {
                ModelState.AddModelError("ImageFiles", "faylin olcusu 2mb-dan artiq ola bilmez!");
                return View();
            }




            slider.Image = FileManager.Save(_enviro.WebRootPath,"uploads/sliders",slider.ImageFiles); 


            _context.HomeSliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            HomeSlider slider = _context.HomeSliders.FirstOrDefault(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            FileManager.Delete(_enviro.WebRootPath, "uploads/sliders", slider.Image);
            _context.HomeSliders.Remove(slider);
            _context.SaveChanges();
            return Ok(); 
        }

        public IActionResult Edit( int id)
        {
            HomeSlider slider = _context.HomeSliders.FirstOrDefault(x => x.Id == id);

            if (slider == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            return View(slider);
        }

        [HttpPost]
        public IActionResult Edit(HomeSlider slider)
        {
            HomeSlider existslider = _context.HomeSliders.FirstOrDefault(x => x.Id == slider.Id);

            if (slider == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            existslider.Title = slider.Title;
            existslider.SubTitle = slider.SubTitle;
            existslider.Desc = slider.Desc;
            existslider.ButtonUrl = slider.ButtonUrl;
            existslider.ButtonText = slider.ButtonText;
            existslider.Order = slider.Order;

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
