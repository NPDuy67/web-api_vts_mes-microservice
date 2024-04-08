using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteChildEntityWhenDeleteMateDefi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_MaterialDefinition_MaterialDefinitionId",
                table: "Operation");

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_MaterialDefinition_MaterialDefinitionId",
                table: "Operation",
                column: "MaterialDefinitionId",
                principalTable: "MaterialDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_MaterialDefinition_MaterialDefinitionId",
                table: "Operation");

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_MaterialDefinition_MaterialDefinitionId",
                table: "Operation",
                column: "MaterialDefinitionId",
                principalTable: "MaterialDefinition",
                principalColumn: "Id");
        }
    }
}
