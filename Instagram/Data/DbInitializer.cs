using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Data
{
    public class DbInitializer
    {
        public static void Initialize(InstagramContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User {Username="admin",Password="adminadmin"},
                new User {Username="callmeparsa",Password="12345678",Name="Parsa",FamilyName="Hejabi"},
                new User {Username="niki13sh",Password="12345678",Name="Niki",FamilyName="Nazaran"}
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

            var posts = new Post[]
            {
                new Post {UserID=1,Caption="Initial Post For Admin",ImagePath="./1.jpg"},
                new Post {UserID=1,Caption="Second Post For Admin",ImagePath="./2.jpg"},
                new Post {UserID=2,Caption="ParsaFirst",ImagePath="./3.jpg"},
                new Post {UserID=3,Caption="NikiFirst",ImagePath="./4.jpg"}
            };

            foreach (Post post in posts)
            {
                context.Posts.Add(post);
            }
            context.SaveChanges();

            var comments = new Comment[]
            {
                new Comment {PostID=1,UserID=1,Content="Im Admin Commenting on my post."},
                new Comment {PostID=2,UserID=1,Content="Im Admin Commenting on Parsa post."},
                new Comment {PostID=3,UserID=2,Content="Parsa on Niki post."}
            };

            foreach (Comment comment in comments)
            {
                context.Comments.Add(comment);
            }
            context.SaveChanges();
        }
    }
}
