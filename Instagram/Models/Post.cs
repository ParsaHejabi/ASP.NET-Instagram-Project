using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class Post
    {
		public int PostID { get; set; }
        public int UserID { get; set; }

		public User User { get; set; }

		[StringLength(400)]
        public string Caption { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        private DateTime? postTime;
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostTime
        {
            get { return postTime ?? DateTime.UtcNow; }
            set { postTime = value; }
        }

        public HashSet<PostLike> PostLikes { get; set; }
		public HashSet<CommentPostUser> CPUs { get; set; }
        
    }
}
