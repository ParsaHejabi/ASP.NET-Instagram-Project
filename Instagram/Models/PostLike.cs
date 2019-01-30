using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
	public class PostLike
	{
		public int ID { get; set; }
        [Display(Name = "Username")]
        public int UserID { get; set; }
        [Display(Name = "Post ID")]
		public int PostID { get; set; }

		public User User { get; set; }
		public Post Post { get; set; }
	}
}
