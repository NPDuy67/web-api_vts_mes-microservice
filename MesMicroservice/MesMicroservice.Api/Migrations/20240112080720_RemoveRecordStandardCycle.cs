using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRecordStandardCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandardCycleTime",
                table: "ManufacturingRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "StandardCycleTime",
                table: "ManufacturingRecords",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
