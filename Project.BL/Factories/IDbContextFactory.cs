using Project.DAL;

namespace Project.BL.Factories
{
    public interface IDbContextFactory
    {
        ProjectDbContext CreateDbContext();
    }
}