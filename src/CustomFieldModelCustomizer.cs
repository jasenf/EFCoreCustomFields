using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace EFCoreCustomFields;

public class CustomFieldModelCustomizer : ModelCustomizer
{
    public CustomFieldModelCustomizer(ModelCustomizerDependencies dependencies) : base(dependencies) { }

    public override void Customize(ModelBuilder modelBuilder, DbContext dbContext)
    {
        base.Customize(modelBuilder, dbContext);

        //modelBuilder.Entity<CustomField>()
        //    .HasMany<CustomFieldValue<ICustomFieldEntity>>()
        //    .WithOne()
        //    .HasForeignKey("CustomFieldId");
    }
}
