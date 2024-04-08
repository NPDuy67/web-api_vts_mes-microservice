using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceRepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Resource_Id",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentClass_Resource_Id",
                table: "EquipmentClass");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialDefinition_Resource_Id",
                table: "MaterialDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialLot_Resource_Id",
                table: "MaterialLot");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceNetworkConnection_Resource_FromResourceId",
                table: "ResourceNetworkConnection");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceNetworkConnection_Resource_ToResourceId",
                table: "ResourceNetworkConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resource",
                table: "Resource");

            migrationBuilder.RenameTable(
                name: "Resource",
                newName: "Resources");

            migrationBuilder.RenameIndex(
                name: "IX_Resource_ResourceId",
                table: "Resources",
                newName: "IX_Resources_ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Resources_Id",
                table: "Equipment",
                column: "Id",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentClass_Resources_Id",
                table: "EquipmentClass",
                column: "Id",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialDefinition_Resources_Id",
                table: "MaterialDefinition",
                column: "Id",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialLot_Resources_Id",
                table: "MaterialLot",
                column: "Id",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_FromResourceId",
                table: "ResourceNetworkConnection",
                column: "FromResourceId",
                principalTable: "Resources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_ToResourceId",
                table: "ResourceNetworkConnection",
                column: "ToResourceId",
                principalTable: "Resources",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Resources_Id",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentClass_Resources_Id",
                table: "EquipmentClass");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialDefinition_Resources_Id",
                table: "MaterialDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialLot_Resources_Id",
                table: "MaterialLot");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_FromResourceId",
                table: "ResourceNetworkConnection");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_ToResourceId",
                table: "ResourceNetworkConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "Resource");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_ResourceId",
                table: "Resource",
                newName: "IX_Resource_ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resource",
                table: "Resource",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Resource_Id",
                table: "Equipment",
                column: "Id",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentClass_Resource_Id",
                table: "EquipmentClass",
                column: "Id",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialDefinition_Resource_Id",
                table: "MaterialDefinition",
                column: "Id",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialLot_Resource_Id",
                table: "MaterialLot",
                column: "Id",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceNetworkConnection_Resource_FromResourceId",
                table: "ResourceNetworkConnection",
                column: "FromResourceId",
                principalTable: "Resource",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceNetworkConnection_Resource_ToResourceId",
                table: "ResourceNetworkConnection",
                column: "ToResourceId",
                principalTable: "Resource",
                principalColumn: "Id");
        }
    }
}
