using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreCustomFields;

public class CustomFieldsOptionsExtension: IDbContextOptionsExtension
{
    public DbContextOptionsExtensionInfo Info => new CustomFieldsOptionsExtensionInfo(this);

    public void ApplyServices(IServiceCollection services)
    {
        services.AddSingleton<IModelCustomizer, CustomFieldModelCustomizer>();
    }

    public void Validate(IDbContextOptions options) { }

    // Other members of IDbContextOptionsExtension...
}