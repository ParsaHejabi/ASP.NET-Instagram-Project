using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
	public class CommentLike
	{
        [Display(Name = "Username")]
        [Required]
        public int UserID { get; set; }

        [Display(Name = "Comment ID")]
        [Required]
		public int CommentID { get; set; }

		public User User { get; set; }
		public Comment Comment { get; set; }
	}
}
