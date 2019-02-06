using Instagram.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Instagram.Data
{
    public class InstagramContext : IdentityDbContext
    {
        public InstagramContext(DbContextOptions<InstagramContext> options) : base(options)
        {
        }

        public DbSet<User> MyUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("MyUsers");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<PostLike>().ToTable("PostLike");
            modelBuilder.Entity<CommentLike>().ToTable("CommentLike");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<PostLike>().HasKey(p => new { p.UserID, p.PostID });
            modelBuilder.Entity<CommentLike>().HasKey(c => new { c.UserID, c.CommentID });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            //LowerCaseUsername();
            AddTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            //LowerCaseUsername();
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            //LowerCaseUsername();
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //LowerCaseUsername();
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var postEntities = ChangeTracker.Entries().Where(x => x.Entity is Post && (x.State == EntityState.Added));

            var commentEntities = ChangeTracker.Entries().Where(x => x.Entity is Comment && (x.State == EntityState.Added));

            foreach (var entity in postEntities)
            {
                ((Post)entity.Entity).PostTime = DateTime.Now;
            }

            foreach (var entity in commentEntities)
            {
                ((Comment)entity.Entity).CommentTime = DateTime.Now;
            }
        }

        //private void LowerCaseUsername()
        //{
        //    var userEntities = ChangeTracker.Entries().Where(x => x.Entity is User && (x.State == EntityState.Added));

        //    foreach (var entity in userEntities)
        //    {
        //        ((User)entity.Entity).UserName = ((User)entity.Entity).UserName.ToLowerInvariant();
        //    }
        //}

        //public bool VerifyUsername(string username)
        //{
        //    var userEntities = this.Users;
        //    foreach (var user in userEntities)
        //    {
        //        if (user.UserName.Equals(username.ToLowerInvariant()))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}
