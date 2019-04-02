using Fitter.DAL;
using Microsoft.EntityFrameworkCore;

namespace Fitter.BL.Factories
{
    public class DbContextFactory : IDbContextFactory
    {
        public FitterDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitterDbContext>();

            optionsBuilder.UseSqlServer(
                @"Data Source = (LocalDB)\MSSQLLocalDB;
                        Initial Catalog = ProjectDB;
                        MultipleActiveResultSets = True;
                        Integrated Security = True ");

            return new FitterDbContext(optionsBuilder.Options);
        }
    }
}