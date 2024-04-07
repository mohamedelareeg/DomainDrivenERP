using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitectureWithDDD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_cancelled_to_baseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Journals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Coas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Coas");
        }
    }
}
