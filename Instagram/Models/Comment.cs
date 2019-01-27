using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class Comment
    {
        [Required]
        public Post CommentedOn { get; set; }

        [Required]
        public User CommentOwner { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CommentTime { get; set; }

        public HashSet<User> UsersLiked { get; set; }
    }
}
