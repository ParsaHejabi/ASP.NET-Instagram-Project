using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class PostViewModel
    {
        [Display(Name = "Username")]
        public int UserID { get; set; }

        [StringLength(400)]
        public string Caption { get; set; }

        public IFormFile Image { get; set; }
    }
}
