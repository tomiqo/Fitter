using Fitter.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Fitter.DAL
{
    public class FitterDbContext : DbContext
    {
        public FitterDbContext()
        {
        }

        public FitterDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasMany<Post>(p => p.Posts)
                .WithOne(t => t.Team)
                .HasForeignKey(k => k.CurrentTeamId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Post>()
                .HasMany<Comment>(c => c.Comments)
                .WithOne(p => p.Post)
                .HasForeignKey(k => k.CurrentPostId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasMany<Post>(p => p.Posts)
                .WithOne(a => a.Author)
                .HasForeignKey(k => k.CurrentAuthorId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<User>()
                .HasMany<Comment>(c => c.Comments)
                .WithOne(a => a.Author)
                .HasForeignKey(k => k.CurrentAuthorId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Post>()
                .HasMany<Attachment>(a => a.Attachments)
                .WithOne(p => p.Post)
                .HasForeignKey(k => k.CurrentPostId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Post>()
                .HasMany(t => t.Tags)
                .WithOne();
            modelBuilder.Entity<Comment>()
                .HasMany(t => t.Tags)
                .WithOne();
            modelBuilder.Entity<UsersInTeam>().HasKey(ut => new {ut.UserId, ut.TeamId});
            modelBuilder.Entity<UsersInTeam>()
                .HasOne(u => u.User)
                .WithMany(t => t.UsersInTeams)
                .HasForeignKey(ut => ut.UserId);
            modelBuilder.Entity<UsersInTeam>()
                .HasOne(t => t.Team)
                .WithMany(ut => ut.UsersInTeams)
                .HasForeignKey(k => k.TeamId);
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersInTeam> UsersInTeams { get; set; } 
    }
}
