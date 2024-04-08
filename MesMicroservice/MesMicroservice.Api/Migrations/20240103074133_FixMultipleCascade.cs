using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixMultipleCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_FromResourceId",
                table: "ResourceNetworkConnection");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_FromResourceId",
                table: "ResourceNetworkConnection",
                column: "FromResourceId",
                principalTable: "Resources",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_FromResourceId",
                table: "ResourceNetworkConnection");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceNetworkConnection_Resources_FromResourceId",
                table: "ResourceNetworkConnection",
                column: "FromResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
