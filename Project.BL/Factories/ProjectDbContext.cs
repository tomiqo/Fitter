using Microsoft.EntityFrameworkCore;

namespace Project.BL.Factories
{
    public class ProjectDbContext : IDbContextProject
    {
        public ProjectDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DAL.ProjectDbContext>();

            optionsBuilder.UseSqlServer(
                @"Data Source = (LocalDB)\MSSQLLocalDB;
                        Initial Catalog = ProjectDB;
                        MultipleActiveResultSets = True;
                        Integrated Security = True ");

            return new ProjectDbContext(optionsBuilder.Options);
        }
    }
}