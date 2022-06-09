using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
