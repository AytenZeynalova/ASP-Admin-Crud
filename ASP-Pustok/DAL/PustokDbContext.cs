
using ASP_Pustok.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.DAL
{
    public class PustokDbContext:DbContext
    {
        public PustokDbContext(DbContextOptions<PustokDbContext> options) :base(options)
        {

        }

        public DbSet<HomeSlider> HomeSliders { get; set; }
        public DbSet<HomeFuture> HomeFutures { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
