using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddManufacturingRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "manufacturingrecordeq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "ManufacturingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    OutputMaterialDefinitionId = table.Column<int>(type: "int", nullable: false),
                    StandardCycleTime = table.Column<double>(type: "float", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Output = table.Column<decimal>(type: "decimal(30,8)", precision: 30, scale: 8, nullable: false),
                    Defects = table.Column<decimal>(type: "decimal(30,8)", precision: 30, scale: 8, nullable: false),
                    OutputUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingRecords_MaterialDefinition_OutputMaterialDefinitionId",
                        column: x => x.OutputMaterialDefinitionId,
                        principalTable: "MaterialDefinition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManufacturingRecords_MaterialUnit_OutputUnitId",
                        column: x => x.OutputUnitId,
                        principalTable: "MaterialUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManufacturingRecords_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentManufacturingRecord",
                columns: table => new
                {
                    EquipmentsId = table.Column<int>(type: "int", nullable: false),
                    ManufacturingRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentManufacturingRecord", x => new { x.EquipmentsId, x.ManufacturingRecordId });
                    table.ForeignKey(
                        name: "FK_EquipmentManufacturingRecord_Equipment_EquipmentsId",
                        column: x => x.EquipmentsId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentManufacturingRecord_ManufacturingRecords_ManufacturingRecordId",
                        column: x => x.ManufacturingRecordId,
                        principalTable: "ManufacturingRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentManufacturingRecord_ManufacturingRecordId",
                table: "EquipmentManufacturingRecord",
                column: "ManufacturingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingRecords_OutputMaterialDefinitionId",
                table: "ManufacturingRecords",
                column: "OutputMaterialDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingRecords_OutputUnitId",
                table: "ManufacturingRecords",
                column: "OutputUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingRecords_WorkOrderId_StartTime",
                table: "ManufacturingRecords",
                columns: new[] { "WorkOrderId", "StartTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentManufacturingRecord");

            migrationBuilder.DropTable(
                name: "ManufacturingRecords");

            migrationBuilder.DropSequence(
                name: "manufacturingrecordeq",
                schema: "application");
        }
    }
}
