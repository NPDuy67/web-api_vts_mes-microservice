using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWorkOrderQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualQuantity",
                table: "WorkOrders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualQuantity",
                table: "WorkOrders",
                type: "decimal(30,8)",
                precision: 30,
                scale: 8,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
