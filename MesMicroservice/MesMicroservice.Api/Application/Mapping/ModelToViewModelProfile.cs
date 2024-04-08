using MesMicroservice.Api.Application.Mapping.Converters;
using MesMicroservice.Api.Application.Queries.Enterprises;
using MesMicroservice.Api.Application.Queries.EquipmentClasses;
using MesMicroservice.Api.Application.Queries.Equipments;
using MesMicroservice.Api.Application.Queries.MaterialDefinitions;
using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;
using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialLotAggregate;
using MesMicroservice.Api.Application.Queries.MaterialLots;
using MesMicroservice.Api.Application.Mapping.Converters.EnterpriseConverter;
using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
using MesMicroservice.Api.Application.Queries.ManufacturingOrders;
using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;
using MesMicroservice.Api.Application.Queries.WorkOrders;
using MesMicroservice.Domain.AggregateModels.ResourceRelationshipNetworkAggregate;
using MesMicroservice.Api.Application.Queries.ResourceRelationshipNetworks;
using MesMicroservice.Api.Application.Queries.ResourceRelationshipNetworks.ResourceNetworkConnections;
using MesMicroservice.Domain.AggregateModels.ManufacucturingRecordAggregate;
using MesMicroservice.Api.Application.Queries.ManufacturingRecords;
using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;
using MesMicroservice.Api.Application.Queries.MaterialClasses;

namespace MesMicroservice.Api.Application.Mapping;
public class ModelToViewModelProfile : Profile
{
    public ModelToViewModelProfile()
    {
        MapEnterpriseViewModels();

        MapMaterialClassViewModels();
        MapMaterialDefinitionViewModels();
        MapMaterialLotViewModels();

        MapEquipmentClassViewModels();
        MapEquipmentViewModels();

        MapManufacturingOrderViewModels();
        MapWorkOrderViewModels();

        MapResourceRelationshipNetworkViewModels();

        MapManufacturingRecordViewModels();
    }

    public void MapEnterpriseViewModels()
    {
        CreateMap<HierarchyModel, string?>()
            .ConvertUsing<HierarchyModelToStringConverter>();

        CreateMap<QueryResult<Enterprise>, QueryResult<EnterpriseViewModel>>();
        CreateMap<Enterprise, EnterpriseViewModel>()
            .ConvertUsing<EnterpriseToEnterpriseViewModelConverter>();

        CreateMap<Site, SiteViewModel>()
            .ConvertUsing<SiteToSiteViewModelConverter>();

        CreateMap<Area, AreaViewModel>()
            .ConvertUsing<AreaToAreaViewModelConverter>();

        CreateMap<WorkCenter, WorkCenterViewModel>()
            .ConvertUsing<WorkCenterToWorkCenterViewModelConverter>();
        CreateMap<WorkCenter, string>()
            .ConvertUsing<WorkCenterToStringConverter>();

        CreateMap<QueryResult<WorkUnit>, QueryResult<WorkUnitViewModel>>();
        CreateMap<WorkUnit, WorkUnitViewModel>()
            .ConvertUsing<WorkUnitToWorkUnitViewModelConverter>();
        CreateMap<WorkUnit, string>()
            .ConvertUsing<WorkUnitToStringConverter>();
    }

    public void MapMaterialClassViewModels()
    {
        CreateMap<QueryResult<MaterialClass>, QueryResult<MaterialClassViewModel>>();
        CreateMap<MaterialClass, MaterialClassViewModel>()
            .ForMember(dest => dest.MaterialClassId, dest => dest.MapFrom(src => src.ResourceId));
        CreateMap<MaterialClass, string>()
            .ConvertUsing<MaterialClassToStringConverter>();
    }

    public void MapMaterialDefinitionViewModels()
    {
        CreateMap<QueryResult<MaterialDefinition>, QueryResult<MaterialDefinitionViewModel>>();
        CreateMap<MaterialDefinition, MaterialDefinitionViewModel>()
            .ForMember(dest => dest.MaterialDefinitionId, dest => dest.MapFrom(src => src.ResourceId));
        CreateMap<MaterialDefinition, string>()
            .ConvertUsing<MaterialDefinitionToStringConverter>();
        CreateMap<MaterialDefinitionProperty, PropertyViewModel>()
            .ForMember(dest => dest.ValueString, dest => dest.MapFrom(src => src.Value.ValueString))
            .ForMember(dest => dest.ValueType, dest => dest.MapFrom(src => src.Value.ValueType));
        CreateMap<MaterialUnit, MaterialUnitViewModel>();
        CreateMap<MaterialUnit, string>()
            .ConvertUsing<MaterialUnitToStringConverter>();
        CreateMap<Operation, OperationViewModel>();
        CreateMap<Operation, string>()
            .ConvertUsing<OperationToStringConverter>();
    }

    public void MapMaterialLotViewModels()
    {
        CreateMap<QueryResult<MaterialLot>, QueryResult<MaterialLotViewModel>>();
        CreateMap<MaterialLot, MaterialLotViewModel>()
            .ForMember(dest => dest.MaterialLotId, dest => dest.MapFrom(src => src.ResourceId));
    }

    public void MapEquipmentClassViewModels()
    {
        CreateMap<QueryResult<EquipmentClass>, QueryResult<EquipmentClassViewModel>>();
        CreateMap<EquipmentClass, EquipmentClassViewModel>()
            .ForMember(dest => dest.EquipmentClassId, dest => dest.MapFrom(src => src.ResourceId));
        CreateMap<EquipmentClass, string>()
            .ConvertUsing<EquipmentClassToStringConverter>();
    }

    public void MapEquipmentViewModels()
    {
        CreateMap<QueryResult<Equipment>, QueryResult<EquipmentViewModel>>();
        CreateMap<Equipment, EquipmentViewModel>()
            .ForMember(dest => dest.EquipmentId, dest => dest.MapFrom(src => src.ResourceId))
            .ForMember(dest => dest.AbsolutePath, dest => dest.MapFrom(src => src.HierarchyModel));
        CreateMap<Equipment, string?>()
            .ConvertUsing<EquipmentToStringConverter>();
        CreateMap<EquipmentProperty, PropertyViewModel>()
            .ForMember(dest => dest.ValueString, dest => dest.MapFrom(src => src.Value.ValueString))
            .ForMember(dest => dest.ValueType, dest => dest.MapFrom(src => src.Value.ValueType));
    }

    public void MapManufacturingOrderViewModels()
    {
        CreateMap<QueryResult<ManufacturingOrder>, QueryResult<ManufacturingOrderViewModel>>();
        CreateMap<ManufacturingOrder, ManufacturingOrderViewModel>();
        CreateMap<ManufacturingOrder, string>()
            .ConvertUsing<ManufacturingOrderToStringConverter>();
    }

    public void MapWorkOrderViewModels()
    {
        CreateMap<QueryResult<WorkOrder>, QueryResult<WorkOrderViewModel>>();
        CreateMap<WorkOrder, WorkOrderViewModel>()
            .ForMember(dest => dest.MaterialDefinition, dest => dest.MapFrom(src => src.ManufacturingOrder.MaterialDefinition.ResourceId));
        CreateMap<WorkOrder, string>()
            .ConvertUsing<WorkOrderToStringConverter>();
    }

    public void MapResourceRelationshipNetworkViewModels()
    {
        CreateMap<QueryResult<ResourceRelationshipNetwork>, QueryResult<ResourceRelationshipNetworkViewModel>>();
        CreateMap<ResourceRelationshipNetwork, ResourceRelationshipNetworkViewModel>();

        CreateMap<QueryResult<ResourceNetworkConnection>, QueryResult<ResourceNetworkConnectionViewModel>>();
        CreateMap<ResourceNetworkConnection, ResourceNetworkConnectionViewModel>();
        CreateMap<ResourceNetworkConnectionProperty, PropertyViewModel>()
            .ForMember(dest => dest.ValueString, dest => dest.MapFrom(src => src.Value.ValueString))
            .ForMember(dest => dest.ValueType, dest => dest.MapFrom(src => src.Value.ValueType));
        CreateMap<Resource, string>()
            .ConvertUsing<ResourceToStringConverter>();
        CreateMap<Resource, ConnectedResourceViewModel>()
            .ForMember(dest => dest.ResourceType, dest => dest.MapFrom(src => src.GetType().Name));
    }

    public void MapManufacturingRecordViewModels()
    {
        CreateMap<QueryResult<ManufacturingRecord>, QueryResult<ManufacturingRecordViewModel>>();
        CreateMap<ManufacturingRecord, ManufacturingRecordViewModel>()
            .ForMember(dest => dest.WorkOrderId, dest => dest.MapFrom(src => src.WorkOrder.WorkOrderId))
            .ForMember(dest => dest.OutputMaterialDefinitionId, dest => dest.MapFrom(src => src.OutputMaterialDefinition.ResourceId))
            .ForMember(dest => dest.EquipmentIds, dest => dest.MapFrom(src => src.Equipments.ConvertAll(x => x.ResourceId)));
    }
}