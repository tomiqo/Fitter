using Microsoft.EntityFrameworkCore;
using Project.DAL;

namespace Project.BL.Factories
{
    public class DbContextFactory : IDbContextFactory
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