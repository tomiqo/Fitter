namespace Project.BL.Factories
{
    public interface IDbContextProject
    {
        ProjectDbContext CreateDbContext();
    }
}