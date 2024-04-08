using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMaterialClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MaterialUnit_UnitId",
                table: "MaterialUnit");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialDefinitionId",
                table: "MaterialUnit",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialClassId",
                table: "MaterialDefinition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MaterialClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialClass_Resources_Id",
                        column: x => x.Id,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialUnit_UnitId_MaterialDefinitionId",
                table: "MaterialUnit",
                columns: new[] { "UnitId", "MaterialDefinitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDefinition_MaterialClassId",
                table: "MaterialDefinition",
                column: "MaterialClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialDefinition_MaterialClass_MaterialClassId",
                table: "MaterialDefinition",
                column: "MaterialClassId",
                principalTable: "MaterialClass",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialDefinition_MaterialClass_MaterialClassId",
                table: "MaterialDefinition");

            migrationBuilder.DropTable(
                name: "MaterialClass");

            migrationBuilder.DropIndex(
                name: "IX_MaterialUnit_UnitId_MaterialDefinitionId",
                table: "MaterialUnit");

            migrationBuilder.DropIndex(
                name: "IX_MaterialDefinition_MaterialClassId",
                table: "MaterialDefinition");

            migrationBuilder.DropColumn(
                name: "MaterialClassId",
                table: "MaterialDefinition");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialDefinitionId",
                table: "MaterialUnit",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialUnit_UnitId",
                table: "MaterialUnit",
                column: "UnitId",
                unique: true);
        }
    }
}
