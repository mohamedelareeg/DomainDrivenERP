using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainDrivenERP.Persistence.Migrations;

/// <inheritdoc />
public partial class add_coa_journal_transaction : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Coas",
            columns: table => new
            {
                HeadCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                HeadName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ParentHeadCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                HeadLevel = table.Column<int>(type: "int", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                IsGl = table.Column<bool>(type: "bit", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Coas", x => x.HeadCode);
                table.ForeignKey(
                    name: "FK_Coas_Coas_ParentHeadCode",
                    column: x => x.ParentHeadCode,
                    principalTable: "Coas",
                    principalColumn: "HeadCode");
            });

        migrationBuilder.CreateTable(
            name: "Journals",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                IsOpening = table.Column<bool>(type: "bit", nullable: false),
                JournalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Journals", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Transactions",
            columns: table => new
            {
                TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                JournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                COAId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                Debit = table.Column<double>(type: "float", nullable: false),
                Credit = table.Column<double>(type: "float", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                table.ForeignKey(
                    name: "FK_Transactions_Coas_COAId",
                    column: x => x.COAId,
                    principalTable: "Coas",
                    principalColumn: "HeadCode");
                table.ForeignKey(
                    name: "FK_Transactions_Journals_JournalId",
                    column: x => x.JournalId,
                    principalTable: "Journals",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Coas_ParentHeadCode",
            table: "Coas",
            column: "ParentHeadCode");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_COAId",
            table: "Transactions",
            column: "COAId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_JournalId",
            table: "Transactions",
            column: "JournalId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Transactions");

        migrationBuilder.DropTable(
            name: "Coas");

        migrationBuilder.DropTable(
            name: "Journals");
    }
}
