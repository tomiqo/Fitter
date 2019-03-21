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
            //Relacia medzi user a team ???, M to N sa tu neda 
            //Prednaska 25.2 1:07:00
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Team>()
                .HasMany(p => p.Posts)
                .WithOne(p => p.Team);

            modelBuilder.Entity<Post>()
                .HasMany(c => c.Comments);
                //.WithOne(c => c.Parent);      NEFUNGUJE ??
        
            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Parent);

            modelBuilder.Entity<Comment>()
                .HasMany(a => a.Attachments)
                .WithOne(a => a.Comment);

            modelBuilder.Entity<Comment>()
                .HasMany(t => t.Tags);
                //.WithOne(t => t.???);
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
