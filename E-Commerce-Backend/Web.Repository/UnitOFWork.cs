namespace Web.Repository;
public class UnitOFWork(WebDbContext context) : IUnitOFWork
{
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
