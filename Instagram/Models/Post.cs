using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public int ID { get; set; }
        [Display(Name = "Username")]
        public int UserID { get; set; }

		public User User { get; set; }

		[StringLength(400)]
        public string Caption { get; set; }

        [Required]
        public String ImagePath { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
		[Display(Name = "Created Time")]
		public DateTime PostTime { get; set; }

        [Display(Name = "Post Likes")]
        public ICollection<PostLike> PostLikes { get; set; }

		public ICollection<Comment> Comments { get; set; }
        
    }
}
