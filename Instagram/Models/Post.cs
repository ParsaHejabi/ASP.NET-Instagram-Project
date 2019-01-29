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
        public int UserID { get; set; }

		public User User { get; set; }

		[StringLength(400)]
        public string Caption { get; set; }

        [Required]
        public Guid ImageName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostTime { get; set; }

        public ICollection<PostLike> PostLikes { get; set; }
		public ICollection<Comment> Comments { get; set; }
        
    }
}
