using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleSample.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Customer",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customer", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CustomFields",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomFieldType = table.Column<int>(type: "int", nullable: false),
                DisplayOrder = table.Column<int>(type: "int", nullable: false),
                IsRequired = table.Column<bool>(type: "bit", nullable: false),
                DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ForEntity = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CustomFields", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CustomerCustomFields",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CustomFieldId = table.Column<long>(type: "bigint", nullable: false),
                ValueString = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                ValueNumber = table.Column<long>(type: "bigint", nullable: true),
                ValueDecimal = table.Column<double>(type: "float", nullable: true),
                ValueBoolean = table.Column<bool>(type: "bit", nullable: true),
                ValueDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CustomerCustomFields", x => x.Id);
                table.ForeignKey(
                    name: "FK_CustomerCustomFields_Customer_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "Customer",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ProductCustomFields",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CustomFieldId = table.Column<long>(type: "bigint", nullable: false),
                ValueString = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                ValueNumber = table.Column<long>(type: "bigint", nullable: true),
                ValueDecimal = table.Column<double>(type: "float", nullable: true),
                ValueBoolean = table.Column<bool>(type: "bit", nullable: true),
                ValueDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                ProductId = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductCustomFields", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProductCustomFields_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_CustomerCustomFields_CustomerId",
            table: "CustomerCustomFields",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductCustomFields_ProductId",
            table: "ProductCustomFields",
            column: "ProductId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CustomerCustomFields");

        migrationBuilder.DropTable(
            name: "CustomFields");

        migrationBuilder.DropTable(
            name: "ProductCustomFields");

        migrationBuilder.DropTable(
            name: "Customer");

        migrationBuilder.DropTable(
            name: "Products");
    }
}
