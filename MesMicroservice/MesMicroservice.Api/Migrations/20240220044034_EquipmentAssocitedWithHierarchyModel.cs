using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentAssocitedWithHierarchyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_WorkUnits_WorkUnitId",
                table: "Equipment");

            migrationBuilder.RenameColumn(
                name: "WorkUnitId",
                table: "Equipment",
                newName: "HierarchyModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipment_WorkUnitId",
                table: "Equipment",
                newName: "IX_Equipment_HierarchyModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_HierarchyModel_HierarchyModelId",
                table: "Equipment",
                column: "HierarchyModelId",
                principalTable: "HierarchyModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_HierarchyModel_HierarchyModelId",
                table: "Equipment");

            migrationBuilder.RenameColumn(
                name: "HierarchyModelId",
                table: "Equipment",
                newName: "WorkUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipment_HierarchyModelId",
                table: "Equipment",
                newName: "IX_Equipment_WorkUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_WorkUnits_WorkUnitId",
                table: "Equipment",
                column: "WorkUnitId",
                principalTable: "WorkUnits",
                principalColumn: "Id");
        }
    }
}
