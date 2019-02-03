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

        [Required]
        [Display(Name = "Post ID")]
        public int PostID { get; set; }

        [Required]
        [Display(Name = "Username")]
        public int UserID { get; set; }

        [Required]
		[StringLength(280, ErrorMessage = "Comments cannot be longer than 280 characters.")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
		[Display(Name = "Created Time")]
		public DateTime CommentTime { get; set; }

        [Display(Name = "Comment Likes")]
        public HashSet<CommentLike> CommentLikes { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }
    }
}
