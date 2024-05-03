using EFCoreCustomFields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreCustomFields;Trusted_Connection=True;MultipleActiveResultSets=true")
            .UseCustomFields();

        return new AppDbContext(optionsBuilder.Options);
    }
}