using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MesMicroservice.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "application");

            migrationBuilder.CreateSequence(
                name: "areaeq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "enterpriseeq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "hierarchymodeleq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "manufacturingordereq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "manufacturingrecordeq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "materialuniteq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "operationeq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "resourceeq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "resourcenetworkconnectioneq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "resourcerelationshipnetworkeq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "siteeq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "workcentereq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "workcenteroutputeq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "workordereq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "workuniteq",
                schema: "application",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "HierarchyModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    HierarchyModelId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HierarchyModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceRelationshipNetworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ResourceRelationshipNetworkId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelationshipType = table.Column<int>(type: "int", nullable: false),
                    RelationshipForm = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceRelationshipNetworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enterprises_HierarchyModel_Id",
                        column: x => x.Id,
                        principalTable: "HierarchyModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enterprises_HierarchyModel_ParentId",
                        column: x => x.ParentId,
                        principalTable: "HierarchyModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentClass_Resources_Id",
                        column: x => x.Id,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialClass_Resources_Id",
                        column: x => x.Id,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceNetworkConnection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromResourceId = table.Column<int>(type: "int", nullable: false),
                    ToResourceId = table.Column<int>(type: "int", nullable: false),
                    ResourceRelationshipNetworkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceNetworkConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceNetworkConnection_ResourceRelationshipNetworks_ResourceRelationshipNetworkId",
                        column: x => x.ResourceRelationshipNetworkId,
                        principalTable: "ResourceRelationshipNetworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceNetworkConnection_Resources_FromResourceId",
                        column: x => x.FromResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceNetworkConnection_Resources_ToResourceId",
                        column: x => x.ToResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sites_Enterprises_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Enterprises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sites_HierarchyModel_Id",
                        column: x => x.Id,
                        principalTable: "HierarchyModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EquipmentClassId = table.Column<int>(type: "int", nullable: false),
                    HierarchyModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipmentClass_EquipmentClassId",
                        column: x => x.EquipmentClassId,
                        principalTable: "EquipmentClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_HierarchyModel_HierarchyModelId",
                        column: x => x.HierarchyModelId,
                        principalTable: "HierarchyModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_Resources_Id",
                        column: x => x.Id,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PrimaryUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialDefinition_MaterialClass_MaterialClassId",
                        column: x => x.MaterialClassId,
                        principalTable: "MaterialClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialDefinition_Resources_Id",
                        column: x => x.Id,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceNetworkConnectionProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Value_ValueString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value_ValueType = table.Column<int>(type: "int", nullable: false),
                    ValueUnitOfMeasure = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceNetworkConnectionProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceNetworkConnectionProperty_ResourceNetworkConnection_ConnectionId",
                        column: x => x.ConnectionId,
                        principalTable: "ResourceNetworkConnection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_HierarchyModel_Id",
                        column: x => x.Id,
                        principalTable: "HierarchyModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Areas_Sites_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Sites",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Value_ValueString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value_ValueType = table.Column<int>(type: "int", nullable: false),
                    ValueUnitOfMeasure = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentProperty_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ManufacturingOrderId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialDefinitionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(30,8)", precision: 30, scale: 8, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingOrders_MaterialDefinition_MaterialDefinitionId",
                        column: x => x.MaterialDefinitionId,
                        principalTable: "MaterialDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialDefinitionProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialDefinitionId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Value_ValueString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value_ValueType = table.Column<int>(type: "int", nullable: false),
                    ValueUnitOfMeasure = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDefinitionProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialDefinitionProperty_MaterialDefinition_MaterialDefinitionId",
                        column: x => x.MaterialDefinitionId,
                        principalTable: "MaterialDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MaterialDefinitionId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ConversionValueToPrimaryUnit = table.Column<decimal>(type: "decimal(30,8)", precision: 30, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialUnit_MaterialDefinition_MaterialDefinitionId",
                        column: x => x.MaterialDefinitionId,
                        principalTable: "MaterialDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MaterialDefinitionId = table.Column<int>(type: "int", nullable: false),
                    OperationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operation_MaterialDefinition_MaterialDefinitionId",
                        column: x => x.MaterialDefinitionId,
                        principalTable: "MaterialDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WorkCenterType = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCenters_Areas_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Areas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCenters_HierarchyModel_Id",
                        column: x => x.Id,
                        principalTable: "HierarchyModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialLot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MaterialDefinitionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(30,8)", precision: 30, scale: 8, nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialLot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialLot_MaterialDefinition_MaterialDefinitionId",
                        column: x => x.MaterialDefinitionId,
                        principalTable: "MaterialDefinition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialLot_MaterialUnit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MaterialUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialLot_Resources_Id",
                        column: x => x.Id,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationOperation",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    PrerequisiteOperationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationOperation", x => new { x.OperationId, x.PrerequisiteOperationId });
                    table.ForeignKey(
                        name: "FK_OperationOperation_Operation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationOperation_Operation_PrerequisiteOperationId",
                        column: x => x.PrerequisiteOperationId,
                        principalTable: "Operation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkCenterOutputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WorkCenterId = table.Column<int>(type: "int", nullable: false),
                    MaterialDefinitionId = table.Column<int>(type: "int", nullable: false),
                    Output = table.Column<decimal>(type: "decimal(30,8)", precision: 30, scale: 8, nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCenterOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCenterOutputs_MaterialDefinition_MaterialDefinitionId",
                        column: x => x.MaterialDefinitionId,
                        principalTable: "MaterialDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkCenterOutputs_MaterialUnit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MaterialUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkCenterOutputs_WorkCenters_WorkCenterId",
                        column: x => x.WorkCenterId,
                        principalTable: "WorkCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WorkOrderId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActuallyStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActuallyEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkOrderStatus = table.Column<int>(type: "int", nullable: false),
                    WorkCenterId = table.Column<int>(type: "int", nullable: true),
                    ManufacturingOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductionTarget = table.Column<decimal>(type: "decimal(30,8)", precision: 30, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_ManufacturingOrders_ManufacturingOrderId",
                        column: x => x.ManufacturingOrderId,
                        principalTable: "ManufacturingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkCenters_WorkCenterId",
                        column: x => x.WorkCenterId,
                        principalTable: "WorkCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkUnits_HierarchyModel_Id",
                        column: x => x.Id,
                        principalTable: "HierarchyModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkUnits_WorkCenters_ParentId",
                        column: x => x.ParentId,
                        principalTable: "WorkCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ManufacturingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    OutputMaterialDefinitionId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Output = table.Column<decimal>(type: "decimal(30,8)", precision: 30, scale: 8, nullable: false),
                    Defects = table.Column<decimal>(type: "decimal(30,8)", precision: 30, scale: 8, nullable: false)
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
                        name: "FK_ManufacturingRecords_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderWorkOrder",
                columns: table => new
                {
                    PrerequisiteOperationsId = table.Column<int>(type: "int", nullable: false),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderWorkOrder", x => new { x.PrerequisiteOperationsId, x.WorkOrderId });
                    table.ForeignKey(
                        name: "FK_WorkOrderWorkOrder_WorkOrders_PrerequisiteOperationsId",
                        column: x => x.PrerequisiteOperationsId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderWorkOrder_WorkOrders_WorkOrderId",
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
                name: "IX_Areas_ParentId",
                table: "Areas",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_ParentId",
                table: "Enterprises",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentClassId",
                table: "Equipment",
                column: "EquipmentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_HierarchyModelId",
                table: "Equipment",
                column: "HierarchyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentManufacturingRecord_ManufacturingRecordId",
                table: "EquipmentManufacturingRecord",
                column: "ManufacturingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentProperty_EquipmentId_PropertyId",
                table: "EquipmentProperty",
                columns: new[] { "EquipmentId", "PropertyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HierarchyModel_HierarchyModelId",
                table: "HierarchyModel",
                column: "HierarchyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingOrders_ManufacturingOrderId",
                table: "ManufacturingOrders",
                column: "ManufacturingOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingOrders_MaterialDefinitionId",
                table: "ManufacturingOrders",
                column: "MaterialDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingRecords_OutputMaterialDefinitionId",
                table: "ManufacturingRecords",
                column: "OutputMaterialDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingRecords_WorkOrderId_StartTime",
                table: "ManufacturingRecords",
                columns: new[] { "WorkOrderId", "StartTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDefinition_MaterialClassId",
                table: "MaterialDefinition",
                column: "MaterialClassId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDefinitionProperty_MaterialDefinitionId_PropertyId",
                table: "MaterialDefinitionProperty",
                columns: new[] { "MaterialDefinitionId", "PropertyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLot_MaterialDefinitionId",
                table: "MaterialLot",
                column: "MaterialDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLot_UnitId",
                table: "MaterialLot",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialUnit_MaterialDefinitionId",
                table: "MaterialUnit",
                column: "MaterialDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialUnit_UnitId_MaterialDefinitionId",
                table: "MaterialUnit",
                columns: new[] { "UnitId", "MaterialDefinitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operation_MaterialDefinitionId",
                table: "Operation",
                column: "MaterialDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_OperationId_MaterialDefinitionId",
                table: "Operation",
                columns: new[] { "OperationId", "MaterialDefinitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationOperation_PrerequisiteOperationId",
                table: "OperationOperation",
                column: "PrerequisiteOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceNetworkConnection_ConnectionId",
                table: "ResourceNetworkConnection",
                column: "ConnectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceNetworkConnection_FromResourceId",
                table: "ResourceNetworkConnection",
                column: "FromResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceNetworkConnection_ResourceRelationshipNetworkId",
                table: "ResourceNetworkConnection",
                column: "ResourceRelationshipNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceNetworkConnection_ToResourceId",
                table: "ResourceNetworkConnection",
                column: "ToResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceNetworkConnectionProperty_ConnectionId_PropertyId",
                table: "ResourceNetworkConnectionProperty",
                columns: new[] { "ConnectionId", "PropertyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRelationshipNetworks_ResourceRelationshipNetworkId",
                table: "ResourceRelationshipNetworks",
                column: "ResourceRelationshipNetworkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceId",
                table: "Resources",
                column: "ResourceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sites_ParentId",
                table: "Sites",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenterOutputs_MaterialDefinitionId",
                table: "WorkCenterOutputs",
                column: "MaterialDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenterOutputs_UnitId",
                table: "WorkCenterOutputs",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenterOutputs_WorkCenterId_MaterialDefinitionId",
                table: "WorkCenterOutputs",
                columns: new[] { "WorkCenterId", "MaterialDefinitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenters_ParentId",
                table: "WorkCenters",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ManufacturingOrderId_WorkOrderId",
                table: "WorkOrders",
                columns: new[] { "ManufacturingOrderId", "WorkOrderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkCenterId",
                table: "WorkOrders",
                column: "WorkCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderWorkOrder_WorkOrderId",
                table: "WorkOrderWorkOrder",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkUnits_ParentId",
                table: "WorkUnits",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentManufacturingRecord");

            migrationBuilder.DropTable(
                name: "EquipmentProperty");

            migrationBuilder.DropTable(
                name: "MaterialDefinitionProperty");

            migrationBuilder.DropTable(
                name: "MaterialLot");

            migrationBuilder.DropTable(
                name: "OperationOperation");

            migrationBuilder.DropTable(
                name: "ResourceNetworkConnectionProperty");

            migrationBuilder.DropTable(
                name: "WorkCenterOutputs");

            migrationBuilder.DropTable(
                name: "WorkOrderWorkOrder");

            migrationBuilder.DropTable(
                name: "WorkUnits");

            migrationBuilder.DropTable(
                name: "ManufacturingRecords");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "ResourceNetworkConnection");

            migrationBuilder.DropTable(
                name: "MaterialUnit");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "EquipmentClass");

            migrationBuilder.DropTable(
                name: "ResourceRelationshipNetworks");

            migrationBuilder.DropTable(
                name: "ManufacturingOrders");

            migrationBuilder.DropTable(
                name: "WorkCenters");

            migrationBuilder.DropTable(
                name: "MaterialDefinition");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "MaterialClass");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Enterprises");

            migrationBuilder.DropTable(
                name: "HierarchyModel");

            migrationBuilder.DropSequence(
                name: "areaeq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "enterpriseeq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "hierarchymodeleq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "manufacturingordereq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "manufacturingrecordeq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "materialuniteq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "operationeq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "resourceeq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "resourcenetworkconnectioneq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "resourcerelationshipnetworkeq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "siteeq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "workcentereq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "workcenteroutputeq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "workordereq",
                schema: "application");

            migrationBuilder.DropSequence(
                name: "workuniteq",
                schema: "application");
        }
    }
}
