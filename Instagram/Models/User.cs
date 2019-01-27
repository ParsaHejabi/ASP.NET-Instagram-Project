using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class User
    {
        [StringLength(30)]
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string FamilyName { get; set; }

        public List<Post> Posts { get; set; }
    }
}
