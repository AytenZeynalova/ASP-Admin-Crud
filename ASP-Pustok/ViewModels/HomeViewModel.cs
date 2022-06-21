using ASP_Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.ViewModels
{
    public class HomeViewModel
    {
        public List<HomeSlider> HomeSliderss { get; set; }
        public List<HomeFuture> HomeFuturess { get; set; }
        public List<Author> Authorss { get; set; }
        public List<Book> DiscountedBooks { get; set; }
        public List<Book> FeaturedBooks { get; set; }
        public List<Book> NewBooks { get; set; }





    }
}
