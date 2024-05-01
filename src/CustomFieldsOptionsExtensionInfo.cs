using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCoreCustomFields;

public class CustomFieldsOptionsExtensionInfo : DbContextOptionsExtensionInfo
{
    public CustomFieldsOptionsExtensionInfo(IDbContextOptionsExtension extension) : base(extension) { }

    public override bool IsDatabaseProvider => false;
    public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => false;
    public override int GetServiceProviderHashCode() => 0;
    public override string LogFragment => "using Eav ";

    public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
    {
        debugInfo["CustomFields"] = "1";
    }

}