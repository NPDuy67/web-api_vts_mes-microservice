using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkOrderEquipmentRequirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationEquipmentRequirement",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    EquipmentClassId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationEquipmentRequirement", x => new { x.OperationId, x.EquipmentClassId });
                    table.ForeignKey(
                        name: "FK_OperationEquipmentRequirement_EquipmentClass_EquipmentClassId",
                        column: x => x.EquipmentClassId,
                        principalTable: "EquipmentClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationEquipmentRequirement_Operation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderEquipmentRequirement",
                columns: table => new
                {
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    EquipmentClassId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderEquipmentRequirement", x => new { x.WorkOrderId, x.EquipmentClassId });
                    table.ForeignKey(
                        name: "FK_WorkOrderEquipmentRequirement_EquipmentClass_EquipmentClassId",
                        column: x => x.EquipmentClassId,
                        principalTable: "EquipmentClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkOrderEquipmentRequirement_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_OperationEquipmentRequirement_EquipmentClassId",
                table: "OperationEquipmentRequirement",
                column: "EquipmentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEquipmentRequirement_EquipmentClassId",
                table: "WorkOrderEquipmentRequirement",
                column: "EquipmentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEquipments_WorkOrderId",
                table: "WorkOrderEquipments",
                column: "WorkOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationEquipmentRequirement");

            migrationBuilder.DropTable(
                name: "WorkOrderEquipmentRequirement");

            migrationBuilder.DropTable(
                name: "WorkOrderEquipments");
        }
    }
}
