using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConnectionWhenDeleteResourceFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_ToResourceId",
                table: "ResourceNetworkConnection");

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
                name: "FK_ResourceNetworkConnection_Resources_ToResourceId",
                table: "ResourceNetworkConnection");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_ToResourceId",
                table: "ResourceNetworkConnection",
                column: "ToResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
