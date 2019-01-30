using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class Comment
    {
		public int ID { get; set; }
        [Display(Name = "Post ID")]
        public int PostID { get; set; }
        [Display(Name = "Username")]
        public int UserID { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }

        [Required]
		[StringLength(280)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
		[Display(Name = "Created Time")]
		public DateTime CommentTime { get; set; }

        public HashSet<CommentLike> CommentLikes { get; set; }
    }
}
