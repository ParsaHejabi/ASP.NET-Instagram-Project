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
        [Required]
        public User PostOwner { get; set; }

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

        public HashSet<User> UsersLiked { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
