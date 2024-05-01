using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCustomFields;

public static class CustomFieldsDbContextOptionsBuilderExtensions
{
  public static DbContextOptionsBuilder UseCustomFields(this DbContextOptionsBuilder optionsBuilder)
    {
        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(new CustomFieldsOptionsExtension());

        return optionsBuilder;
    }
}
