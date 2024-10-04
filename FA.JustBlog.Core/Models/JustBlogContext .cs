using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FA.JustBlog.Core.Models
{
    public class JustBlogContext : IdentityDbContext<ApplicationUser>
    {
        public JustBlogContext(DbContextOptions<JustBlogContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=TRIENTM; Database=JustBlog4; Encrypt=true; TrustServerCertificate=true; Integrated Security=true;");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship without explicit join entity
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTagMap", // Name of the join table
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                    j => j.HasOne<Post>().WithMany().HasForeignKey("PostId")
                );

            // Seed data for categories
            List<Category> categories = new List<Category>
    {
        new Category { Id = 1, Name = "Entity Framework", Description = "All post in Entity Framework", UrlSlug = "entity-framework" },
        new Category { Id = 2, Name = "ASP.NET Core", Description = "All posts in ASP.NET Core", UrlSlug = "asp-net-core" },
        new Category { Id = 3, Name = "ASP.NET Entity", Description = "All posts in ASP.NET Core entity", UrlSlug = "asp-net-core-entity" }
    };
            modelBuilder.Entity<Category>().HasData(categories);

            // Seed data for posts
            List<Post> posts = new List<Post>
    {
        new Post
        {
            Id = 1,
            CategoryId = categories[0].Id,
            Title = "Data Annotations - InverseProperty Attribute in EF 6 & EF Core",
            ShortDescription = "The InverseProperty attribute is used when two entities have more than one relationship.",
            Modified = DateTime.Now,
            PostedOn = DateTime.Now,
            Meta = "EF, MVC",
            PostContent = "In the above example, the Course and Teacher entities have two one-to-many relationships...",
            Published = true,
            RateCount = 10,
            TotalRate = 45,
            UrlSlug = "data-annotation-inverse-property-attribule-in-ef-6",
            ViewCount = 100,
        },
        new Post
        {
            Id = 2,
            Title = "Introduction to ASP.NET Core",
            ShortDescription = "An introduction to ASP.NET Core",
            PostContent = "Content of the post",
            Meta = "ASP.NET Core, .NET",
            UrlSlug = "intro-asp-net-core",
            Published = true,
            PostedOn = DateTime.Now,
            CategoryId = categories[1].Id
        },
        new Post
        {
            Id = 3,
            Title = "Advanced C# Techniques",
            ShortDescription = "A look at advanced C# techniques",
            PostContent = "Content of the post",
            Meta = "C#, .NET",
            UrlSlug = "advanced-c-sharp",
            Published = true,
            PostedOn = DateTime.Now,
            CategoryId = categories[2].Id
        },
        new Post
        {
            Id = 4,
            Title = "Latest Post",
            ShortDescription = "A look at advanced C# techniques",
            PostContent = "Content of the post",
            Meta = "C#, .NET",
            UrlSlug = "advanced-c-sharp",
            Published = true,
            PostedOn = DateTime.Now,
            CategoryId = categories[2].Id
        }
    };

            modelBuilder.Entity<Post>().HasData(posts);

            // Seed data for tags
            List<Tag> tags = new List<Tag>
    {
        new Tag { Id = 1, Name = "Entity Framework", Description = "Entity Framework", Count = 100, UrlSlug = "entity-framework" },
        new Tag { Id = 2, Name = "MVC", Description = "Microsoft MVC", Count = 50, UrlSlug = "mvc" },
        new Tag { Id = 3, Name = "C#", Count = 1, UrlSlug = "c-sharp", Description = "Posts about C#" },
        new Tag { Id = 4, Name = "ASP.NET Core", Count = 1, UrlSlug = "asp-net-core", Description = "Posts about ASP.NET Core" }
    };

            modelBuilder.Entity<Tag>().HasData(tags);

            // Seed data for the many-to-many relationship
            modelBuilder.Entity("PostTagMap").HasData(
                new { PostId = posts[0].Id, TagId = tags[0].Id },
                new { PostId = posts[0].Id, TagId = tags[1].Id },
                new { PostId = posts[1].Id, TagId = tags[2].Id },
                new { PostId = posts[2].Id, TagId = tags[3].Id }
            );

            // Seed data for comments
            List<Comment> comments = new List<Comment>
    {
        new Comment
        {
            Id = 1,
            PostId = posts[0].Id,
            Name = "Scott Trinh",
            Email = "tutb@live.com",
            CommentTime = DateTime.Now,
            CommentHeader = "This is a sample comment",
            CommentText = "This is a sample comment with multiple lines"
        },
        new Comment
        {
            Id = 2,
            Name = "Jane Smith",
            Email = "jane@example.com",
            PostId = posts[1].Id,
            CommentHeader = "Very Informative",
            CommentText = "I learned a lot from this post.",
            CommentTime = DateTime.Now
        },
        new Comment
        {
            Id = 3,
            Name = "Bob Johnson",
            Email = "bob@example.com",
            PostId = posts[2].Id,
            CommentHeader = "Excellent Read",
            CommentText = "Thanks for the great tips.",
            CommentTime = DateTime.Now
        }
    };

            modelBuilder.Entity<Comment>().HasData(comments);
        }

    }
}
