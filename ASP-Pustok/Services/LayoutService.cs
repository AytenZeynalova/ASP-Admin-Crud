using ASP_Pustok.DAL;
using ASP_Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.Services
{
    public class LayoutService
    {
        private PustokDbContext _context;

        public LayoutService(PustokDbContext context)
        {
            _context = context;
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.ToDictionary(x=>x.Key,x=>x.Value);
        }
    }
}
