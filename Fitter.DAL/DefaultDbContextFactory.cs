using Microsoft.EntityFrameworkCore;

namespace Fitter.DAL
{
    public class DefaultDbContextFactory : IFitterDbContext
    {
        public FitterDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitterDbContext>();

            optionsBuilder.UseSqlServer(
                        @"Data Source = (LocalDB)\MSSQLLocalDB;
                        Initial Catalog = FitterDB;
                        MultipleActiveResultSets = True;
                        Integrated Security = True ");

            return new FitterDbContext(optionsBuilder.Options);
        }
    }
}
