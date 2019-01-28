﻿// <auto-generated />
using System;
using Instagram.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Instagram.Migrations
{
    [DbContext(typeof(InstagramContext))]
    partial class InstagramContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Instagram.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CommentTime");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int>("PostID");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("PostID");

                    b.HasIndex("UserID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Instagram.Models.CommentLike", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommentID");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("CommentID");

                    b.HasIndex("UserID");

                    b.ToTable("CommentLike");
                });

            modelBuilder.Entity("Instagram.Models.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasMaxLength(400);

                    b.Property<Guid>("ImageName");

                    b.Property<DateTime>("PostTime");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Instagram.Models.PostLike", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostID");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("PostID");

                    b.HasIndex("UserID");

                    b.ToTable("PostLike");
                });

            modelBuilder.Entity("Instagram.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FamilyName")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Instagram.Models.Comment", b =>
                {
                    b.HasOne("Instagram.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Instagram.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Instagram.Models.CommentLike", b =>
                {
                    b.HasOne("Instagram.Models.Comment", "Comment")
                        .WithMany("CommentLikes")
                        .HasForeignKey("CommentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Instagram.Models.User", "User")
                        .WithMany("CommentLikes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Instagram.Models.Post", b =>
                {
                    b.HasOne("Instagram.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Instagram.Models.PostLike", b =>
                {
                    b.HasOne("Instagram.Models.Post", "Post")
                        .WithMany("PostLikes")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Instagram.Models.User", "User")
                        .WithMany("PostLikes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
