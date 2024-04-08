using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveModuleTypeInMaterialDefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModuleType",
                table: "MaterialDefinition");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModuleType",
                table: "MaterialDefinition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
