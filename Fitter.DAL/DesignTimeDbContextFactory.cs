using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fitter.DAL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FitterDbContext>
    {
        public FitterDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitterDbContext>();

            optionsBuilder.UseSqlServer(
                @"Data Source = (localdb)\MSSQLLocalDB;
                        Initial Catalog = FitterDB;
                        MultipleActiveResultSets = True;
                        Integrated Security = True ");

            return new FitterDbContext(optionsBuilder.Options);
        }
    }
}
