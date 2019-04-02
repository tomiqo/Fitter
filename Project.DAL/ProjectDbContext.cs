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
                .WithMany(p => p.Posts)
                .HasForeignKey(k => k.CurrentTeamId);
            modelBuilder.Entity<Comment>()
                .HasOne<Post>(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.CurrentPostId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>()
                .HasOne<User>(u => u.Author)
                .WithMany(p => p.Posts)
                .HasForeignKey(k => k.CurrentAuthorId);
            modelBuilder.Entity<Comment>()
                .HasOne<User>(u => u.Author)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.CurrentAuthorId);
            modelBuilder.Entity<Attachment>()
                .HasOne<Post>(p => p.Post)
                .WithMany(a => a.Attachments)
                .HasForeignKey(k => k.CurrentPostId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>()
                .HasMany(t => t.Tags).WithOne();
            modelBuilder.Entity<Comment>()
                .HasMany(t => t.Tags).WithOne();
            modelBuilder.Entity<UsersInTeam>().HasKey(ut => new {ut.UserId, ut.TeamId});
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersInTeam> UsersInTeams { get; set; } 
    }
}
