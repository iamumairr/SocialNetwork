using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;

namespace SocialNetwork.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<MessagePost> Messages { get; set; }
        public DbSet<PhotoPost> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
