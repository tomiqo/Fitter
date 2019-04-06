using Fitter.BL.Factories;
using Fitter.DAL;
using Microsoft.EntityFrameworkCore;

namespace Fitter.BL.Tests
{
    public class InMemoryDbContext : IDbContextFactory
    {
        public FitterDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitterDbContext>();
            optionsBuilder.UseInMemoryDatabase("FitterDB");
            return new FitterDbContext(optionsBuilder.Options);
        }
    }
}