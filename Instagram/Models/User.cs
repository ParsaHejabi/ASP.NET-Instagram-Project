using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class User : IdentityUser
    {
        //public int ID { get; set; }

        //[StringLength(30)]
        //[Required]
        //[RegularExpression(@"^[a-zA-Z]+[a-zA-Z\d_-]*$")]
        //[Remote(action: "VerifyUsername", controller: "Users")]
        //public string Username { get; set; }

        //[DataType(DataType.Password)]
        //[StringLength(30, MinimumLength = 5)]
        //[Required]
        //public string Password { get; set; }

        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        [DisplayFormat(NullDisplayText = "No first name")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        [DisplayFormat(NullDisplayText = "No last name")]
        public string FamilyName { get; set; }

        [Display(Name = "Full Name")]
        public string Fullname
        {
            get
            {
                return Name + ", " + FamilyName;
            }
        }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
    }
}
