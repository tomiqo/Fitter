using Microsoft.EntityFrameworkCore;

namespace Project.DAL.Tests
{
    public class InMemoryProjectDbContext : IDbContextProject
    {
        public ProjectDbContext CreateDbContext()
        {
            var optionBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            optionBuilder.UseInMemoryDatabase("ProjectDbName");
            return new ProjectDbContext(optionBuilder.Options);
        }
    }
}