using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class Comment
    {
		public int CommentID { get; set; }

		public Post post;
		public User user;

        [Required]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CommentTime { get; set; }

		public HashSet<CommentPostUser> CPUs { get; set; }
        public HashSet<CommnetLike> CommnetLikes { get; set; }
    }
}
