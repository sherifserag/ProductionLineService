using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductionLineService.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseAlteration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ProductionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "ProductionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "ProductionLogs");

            migrationBuilder.DropColumn(
                name: "Operator",
                table: "ProductionLogs");
        }
    }
}
