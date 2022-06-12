using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Pustok.Models
{
    public class HomeSlider
    {
        public int Id { get; set; }
        public int Order { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50)]

        public string SubTitle { get; set; }
        [MaxLength(350)]

        public string Desc { get; set; }
        [MaxLength(50)]

        public string ButtonText { get; set; }
        
        public string ButtonUrl { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFiles { get; set; }


    }
}
