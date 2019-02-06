using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
	public class PostLike
	{
        [Display(Name = "Username")]
        [Required]
        public string UserID { get; set; }

        [Display(Name = "Post ID")]
        [Required]
		public int PostID { get; set; }

		public User User { get; set; }
		public Post Post { get; set; }
	}
}
