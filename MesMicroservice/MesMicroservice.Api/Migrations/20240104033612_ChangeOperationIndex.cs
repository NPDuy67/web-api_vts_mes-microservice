using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOperationIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operation_OperationId",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Operation");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialDefinitionId",
                table: "Operation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operation_OperationId_MaterialDefinitionId",
                table: "Operation",
                columns: new[] { "OperationId", "MaterialDefinitionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operation_OperationId_MaterialDefinitionId",
                table: "Operation");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialDefinitionId",
                table: "Operation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "Operation",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Operation_OperationId",
                table: "Operation",
                column: "OperationId",
                unique: true);
        }
    }
}
