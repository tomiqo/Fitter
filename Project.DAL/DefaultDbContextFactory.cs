using Microsoft.EntityFrameworkCore;

namespace Project.DAL
{
    public class DefaultDbContextFactory : IDbContextProject
    {
        public ProjectDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();

            optionsBuilder.UseSqlServer(
                        @"Data Source = (LocalDB)\MSSQLLocalDB;
                        Initial Catalog = ProjectDB;
                        MultipleActiveResultSets = True;
                        Integrated Security = True ");

            return new ProjectDbContext(optionsBuilder.Options);
        }
    }
}
