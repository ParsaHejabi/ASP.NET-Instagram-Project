using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
	public class CommentLike
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public int CommentID { get; set; }

		public User User { get; set; }
		public Comment Comment { get; set; }
	}
}
