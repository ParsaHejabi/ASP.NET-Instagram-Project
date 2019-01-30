using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class User
    {
		public int ID { get; set; }

        [StringLength(30)]
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }

        [StringLength(50)]
		[Display(Name = "First Name")]
		public string Name { get; set; }

        [StringLength(50)]
		[Display(Name = "Last Name")]
		public string FamilyName { get; set; }

        public ICollection<Post> Posts { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<PostLike> PostLikes { get; set; }
		public ICollection<CommentLike> CommentLikes { get; set; }
	}
}
