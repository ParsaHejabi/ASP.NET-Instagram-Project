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
        [Required]
        public int UserID { get; set; }

		[StringLength(400, ErrorMessage = "Caption cannot be longer than 400 characters.")]
        [DisplayFormat(NullDisplayText = "No caption")]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Image Path")]
        public String ImagePath { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
		[Display(Name = "Created Time")]
		public DateTime PostTime { get; set; }

        [Display(Name = "Post Likes")]
        public ICollection<PostLike> PostLikes { get; set; }

		public ICollection<Comment> Comments { get; set; }

        public User User { get; set; }
    }
}
