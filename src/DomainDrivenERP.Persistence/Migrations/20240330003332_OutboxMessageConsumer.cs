using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainDrivenERP.Persistence.Migrations;

/// <inheritdoc />
public partial class OutboxMessageConsumer : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "OutboxMessageConsumers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_OutboxMessageConsumers", x => new { x.Id, x.Name }));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "OutboxMessageConsumers");
    }
}
