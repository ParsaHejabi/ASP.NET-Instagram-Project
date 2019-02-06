using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class CommentViewModel
    {
        [Required]
        [StringLength(280, ErrorMessage = "Comments cannot be longer than 280 characters.")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Post ID")]
        public int PostID { get; set; }
    }
}
