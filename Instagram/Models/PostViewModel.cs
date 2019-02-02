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
        [Required]
        public int UserID { get; set; }

        [StringLength(400,ErrorMessage = "Caption cannot be longer than 400 characters.")]
        public string Caption { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
