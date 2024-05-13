using MesMicroservice.Api.Application.Exceptions;
using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;
using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Api.Application.Commands.WorkOrders;

public class CreateWorkOrderCommandHandler : IRequestHandler<CreateWorkOrderCommand, bool>
{
    private readonly IManufacturingOrderRepository _manufacturingOrderRepository;
    private readonly IWorkOrderRepository _workOrderRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;

    public CreateWorkOrderCommandHandler(IManufacturingOrderRepository manufacturingOrderRepository, IWorkOrderRepository workOrderRepository, IEnterpriseRepository enterpriseRepository)
    {
        _manufacturingOrderRepository = manufacturingOrderRepository;
        _workOrderRepository = workOrderRepository;
        _enterpriseRepository = enterpriseRepository;
    }

    public async Task<bool> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var manufacturingOrder = await _manufacturingOrderRepository.GetAsync(request.ManufacturingOrderId) ?? throw new ResourceNotFoundException(nameof(ManufacturingOrder), request.ManufacturingOrderId);
        var workCenter = await GetWorkCenterByAbsolutePath(request.WorkCenter);
        var prerequisiteOperations = await _workOrderRepository.GetListByIdAsync(manufacturingOrder.Id, request.PrerequisiteOperations);

        var workOrder = new WorkOrder(request.WorkOrderId, request.DueDate, request.EndTime - request.StartTime, request.StartTime, request.EndTime, request.WorkOrderStatus, prerequisiteOperations, workCenter, manufacturingOrder.Quantity);
        manufacturingOrder.AddWorkOrder(workOrder);

        await _workOrderRepository.Add(manufacturingOrder.Id, workOrder);

        return await _workOrderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private async Task<WorkCenter?> GetWorkCenterByAbsolutePath(string? absolutePath)
    {
        if (string.IsNullOrEmpty(absolutePath))
        {
            return null;
        }

        var hierarchyModelIds = absolutePath.Split('/');
        var enterprise = await _enterpriseRepository.GetAsync(hierarchyModelIds[0]) ?? throw new ResourceNotFoundException(nameof(Enterprise), hierarchyModelIds[0]);
        var workCenter = enterprise.Sites
            .SelectMany(x => x.Areas)
            .SelectMany(x => x.WorkCenters)
            .FirstOrDefault(x => x.AbsolutePath == absolutePath) ?? throw new ResourceNotFoundException(nameof(WorkCenter), hierarchyModelIds[3]);

        return workCenter;
    }
}
