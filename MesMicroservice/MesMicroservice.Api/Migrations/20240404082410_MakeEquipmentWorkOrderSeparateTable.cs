using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class MakeEquipmentWorkOrderSeparateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderEquipments");

            migrationBuilder.CreateTable(
                name: "EquipmentWorkOrder",
                columns: table => new
                {
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentWorkOrder", x => new { x.WorkOrderId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_EquipmentWorkOrder_Equipment_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "Equipment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipmentWorkOrder_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentWorkOrder");

            migrationBuilder.CreateTable(
                name: "WorkOrderEquipments",
                columns: table => new
                {
                    AssignedEquipmentsId = table.Column<int>(type: "int", nullable: false),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderEquipments", x => new { x.AssignedEquipmentsId, x.WorkOrderId });
                    table.ForeignKey(
                        name: "FK_WorkOrderEquipments_Equipment_AssignedEquipmentsId",
                        column: x => x.AssignedEquipmentsId,
                        principalTable: "Equipment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkOrderEquipments_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEquipments_WorkOrderId",
                table: "WorkOrderEquipments",
                column: "WorkOrderId");
        }
    }
}
