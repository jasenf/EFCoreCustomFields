using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EFCoreCustomFields;

// to recreate initial migration:  dotnet ef migrations add Initial --project Tests\ConsoleSample
// to update database: dotnet ef database update --project Tests\ConsoleSample

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                    "Server=(localdb)\\mssqllocaldb;Database=EFCoreCustomFields;Trusted_Connection=True;MultipleActiveResultSets=true",
                    options =>
                    {
                        options.EnableRetryOnFailure();
                    })
                );

        var provider = services.BuildServiceProvider();

        using (var scope = provider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Create the database during startup.
            context.Database.Migrate();

            // create the first custom field if it doesnt exist
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
           
                context.CustomFields.Add(new CustomField
                {
                    Name = "Size",
                    CustomFieldType = CustomFieldType.Number,
                    DisplayOrder = 2,
                    IsRequired = true,
                    ForEntity = Product.EntityName
                });
           
                context.CustomFields.Add(new CustomField
                {
                    Name = "Location",
                    CustomFieldType = CustomFieldType.SingleLineText,
                    DisplayOrder = 1,
                    IsRequired = true,
                    ForEntity = Customer.EntityName
                });
            }

            context.SaveChanges();

            // get the custom fields just created one at a time
            var cfColor = context.CustomFields.First(cf => cf.Name == "Color");
            var cfSize = context.CustomFields.First(cf => cf.Name == "Size");
            var cfLocation = context.CustomFields.First(cf => cf.Name == "Location");

            // create a product with a custom field value if it doesnt exist
            if (!context.Products.Any())
            {
                var product = new Product
                {
                    Name = "Product 1",
                    CustomFields = new List<CustomFieldValue<Product>>
                    {
                        new CustomFieldValue<Product>(cfColor.CustomFieldType, cfColor.Id, "Red"),
                        new CustomFieldValue<Product>(cfSize.CustomFieldType, cfSize.Id, 10)
                    }
                };
                context.Products.Add(product);
            }

            if (!context.Customer.Any())
            {
                var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Customer 1",
                    CustomFields = new List<CustomFieldValue<Customer>>
                    {
                        new CustomFieldValue<Customer>(cfLocation.CustomFieldType, cfLocation.Id, "New York")
                    }
                };
                context.Customer.Add(customer);
            }

            context.SaveChanges();
            
            
            // read back the product and customer along with their custom fields
            // then output them to console as json
            var products = context.Products.Include(p => p.CustomFields).ToList();
            var customers = context.Customer.Include(c => c.CustomFields).ToList();

            Console.WriteLine("Products:");
            Console.WriteLine(JsonSerializer.Serialize(products, new JsonSerializerOptions {WriteIndented = true}));

            Console.WriteLine("\nCustomers:");
            Console.WriteLine(JsonSerializer.Serialize(customers, new JsonSerializerOptions {WriteIndented = true}));


            Console.WriteLine("\nDone");

            Console.WriteLine("\nYou can view the EFCoreCustomFields database in SQL Server Object Explorer or SSMS to see how the custom fields are represented there as well.");

            Console.WriteLine("\nNOTE: When the application stops, the sample database will be deleted.  It is created when the sample application starts and dropped when the sample application stops to clean up resources.");

            Console.WriteLine("\nPress any key to close the application.");

            Console.ReadKey();

            // Delete the database at the end of the test run.
            context.Database.EnsureDeleted();
        }
    }
}


public class Product : ICustomFieldEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public static string EntityName  => nameof(Product);
    public ICollection<CustomFieldValue<Product>> CustomFields { get; set; } = new List<CustomFieldValue<Product>>();
}

public class Customer : ICustomFieldEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public static string EntityName => nameof(Customer);
    public ICollection<CustomFieldValue<Customer>> CustomFields { get; set; } = new List<CustomFieldValue<Customer>>();
}

public class AppDbContext : DbContext
{
    public DbSet<CustomField> CustomFields { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CustomFieldValue<Product>> ProductCustomFields { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<CustomFieldValue<Customer>> CustomerCustomFields { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(p => p.CustomFields)
            .WithOne()
            .HasForeignKey("ProductId");

        modelBuilder.Entity<Customer>()
            .HasMany(p => p.CustomFields)
            .WithOne()
            .HasForeignKey("CustomerId");

        base.OnModelCreating(modelBuilder);
    }
}
