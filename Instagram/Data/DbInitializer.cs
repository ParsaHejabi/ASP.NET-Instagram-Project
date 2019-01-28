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
        }
    }
}
