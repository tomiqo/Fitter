using Project.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Project.DAL
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
        {
        }

        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne<Team>(t => t.Team)
                .WithMany(p => p.Posts);
            modelBuilder.Entity<Comment>()
                .HasOne<User>(u => u.Author)
                .WithMany(c => c.Comments);
            modelBuilder.Entity<Comment>()
                .HasOne<Comment>(c => c.Parent)
                .WithMany(s => s.Comments);
            modelBuilder.Entity<Attachment>()
                .HasOne<Comment>(c => c.Comment)
                .WithMany(a => a.Attachments);
            modelBuilder.Entity<Comment>()
                .HasMany(t => t.Tags);
            modelBuilder.Entity<Post>()
                .HasMany<Comment>(c => c.Comments)
                .WithOne(p => p.Post);
            modelBuilder.Entity<Post>()
                .HasOne<User>(a => a.Author)
                .WithMany(p => p.Posts);
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersInTeam> UsersInTeams { get; set; } // ???
    }
}
