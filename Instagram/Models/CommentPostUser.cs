using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
	public class CommentPostUser
	{
		public int CommentPostUserID { get; set; }
		public int CommentID { get; set; }
		public int PostID { get; set; }
		public int UserID { get; set; }

		public Comment Comment { get; set; }
		public Post Post { get; set; }
		public User User { get; set; }
	}
}
