using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnitFromManRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManufacturingRecords_MaterialUnit_OutputUnitId",
                table: "ManufacturingRecords");

            migrationBuilder.DropIndex(
                name: "IX_ManufacturingRecords_OutputUnitId",
                table: "ManufacturingRecords");

            migrationBuilder.DropColumn(
                name: "OutputUnitId",
                table: "ManufacturingRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OutputUnitId",
                table: "ManufacturingRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingRecords_OutputUnitId",
                table: "ManufacturingRecords",
                column: "OutputUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManufacturingRecords_MaterialUnit_OutputUnitId",
                table: "ManufacturingRecords",
                column: "OutputUnitId",
                principalTable: "MaterialUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
