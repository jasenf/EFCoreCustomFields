using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EFCoreCustomFields;  // Assuming the library is named MyEavLibrary

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreCustomFields;Trusted_Connection=True;MultipleActiveResultSets=true")
                   .UseCustomFields());

        var provider = services.BuildServiceProvider();

        using (var scope = provider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // create a custom field if it doesnt exist
            if (!context.CustomFields.Any())
            {
                context.CustomFields.Add(new CustomField
                {
                    Name = "Color",
                    CustomFieldType = CustomFieldType.SingleLineText,
                    DisplayOrder = 1,
                    IsRequired = true,
                    ForEntity = Product.EntityName
                });

                context.SaveChanges();
            }

            // load the custom field ID
            var customFieldId = context.CustomFields.First().Id;

            // create a product with a custom field value if it doesnt exist
            if (!context.Products.Any())
            {
                var product = new Product
                {
                    Name = "Product 1",
                    CustomFields = new List<CustomFieldValue<Product>>
                    {
                        new CustomFieldValue<Product>
                        {
                            CustomFieldId = customFieldId, 
                            ValueString = "Red"
                        }
                    }
                };

                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        Console.WriteLine("Done");
    }
}

public class Product : ICustomFieldEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public static string EntityName  => nameof(Product);
    public ICollection<CustomFieldValue<Product>> CustomFields { get; set; } = new List<CustomFieldValue<Product>>();
}

public class AppDbContext : DbContext
{
    public DbSet<CustomField> CustomFields { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CustomFieldValue<Product>> ProductCustomFields { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(p => p.CustomFields)
            .WithOne()
            .HasForeignKey("CustomFieldEntityId");

        base.OnModelCreating(modelBuilder);
    }
}
