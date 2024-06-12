using Microsoft.EntityFrameworkCore.Design;

namespace TimeTable.Core;
public class DesignTimeContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        return new DataContext();
    }
}
