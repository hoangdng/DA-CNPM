using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWeb.Models;
using System.Security.Cryptography.X509Certificates;

namespace PetWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<UserPost>().HasKey(up => new { up.CustomUserId, up.PostId });
            builder.Entity<UserPost>().HasOne<CustomUser>(up => up.CustomUser).WithMany(u => u.UserPosts).HasForeignKey(up => up.CustomUserId);
            builder.Entity<UserPost>().HasOne<Post>(up => up.Post).WithMany(p => p.UserPosts).HasForeignKey(up => up.PostId);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<UserPost> UserPosts { get; set; }
    }
}
