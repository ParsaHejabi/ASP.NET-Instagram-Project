using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
	public class PostLike
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public int PostID { get; set; }

		public User User { get; set; }
		public Post Post { get; set; }
	}
}
