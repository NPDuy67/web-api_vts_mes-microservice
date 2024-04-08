using MesMicroservice.Api.Application.Buffers;
using MesMicroservice.Api.Application.Exceptions;
using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;
using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Api.Application.Commands.ManufacturingOrders;

public class CreateManufacturingOrderCommandHandler : IRequestHandler<CreateManufacturingOrderCommand, bool>
{
    private readonly IManufacturingOrderRepository _manufacturingOrderRepository;
    private readonly IMaterialDefinitionRepository _materialDefinitionRepository;
    private readonly DueDateBuffer _dueDateBuffer;

    public CreateManufacturingOrderCommandHandler(IManufacturingOrderRepository manufacturingOrderRepository, IMaterialDefinitionRepository materialDefinitionRepository, DueDateBuffer dueDateBuffer)
    {
        _manufacturingOrderRepository = manufacturingOrderRepository;
        _materialDefinitionRepository = materialDefinitionRepository;
        _dueDateBuffer = dueDateBuffer;
    }

    public async Task<bool> Handle(CreateManufacturingOrderCommand request, CancellationToken cancellationToken)
    {
        var materialDefinition = await _materialDefinitionRepository.GetAsync(request.MaterialDefinitionId) ?? throw new ResourceNotFoundException(nameof(MaterialDefinition), request.MaterialDefinitionId);
        var manufacturingOrder = new ManufacturingOrder(request.ManufacturingOrderId, materialDefinition, request.Quantity, request.Unit, request.DueDate, request.AvailableDate, request.Priority);

        await CreateWorkOrders(manufacturingOrder, materialDefinition.Operations);
        await _manufacturingOrderRepository.Add(manufacturingOrder);

        return await _manufacturingOrderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private async Task CreateWorkOrders(ManufacturingOrder manufacturingOrder,List<Operation> operations)
    {
        var workOrders = manufacturingOrder.WorkOrders;
        workOrders.AddRange(operations.Select(x => new WorkOrder(x.OperationId)));

        foreach (var operation in operations)
        {
            var workOrder = workOrders.Find(x => x.WorkOrderId == operation.OperationId);
            var prerequisiteOperations = workOrders
                .Where(x => operation.PrerequisiteOperation.Exists(op => op.OperationId == x.WorkOrderId))
                .ToList();

            workOrder?.SetPrerequisiteOperations(prerequisiteOperations);
            workOrder?.SetDuration(TimeSpan.FromHours(3600));
            workOrder?.SetProductionTarget(manufacturingOrder.Quantity);
        }

        var prerequisiteWorkOrders = workOrders.SelectMany(x => x.PrerequisiteOperations);
        var lastestWorkOrders = workOrders.Except(prerequisiteWorkOrders);

        workOrders.ForEach(x => _dueDateBuffer.Add(x.WorkOrderId));

        foreach (var workOrder in lastestWorkOrders)
        {
            await SetWorkOrderTime(workOrder, manufacturingOrder.DueDate);
        }

        workOrders.ForEach(x => _dueDateBuffer.Remove(x.WorkOrderId));
    }

    private async Task SetWorkOrderTime(WorkOrder workOrder, DateTime dueDate)
    {
        DateTime prereqDueDate = _dueDateBuffer.GetPrereqDueDate(workOrder.WorkOrderId);

        if (dueDate < prereqDueDate)
        {
            workOrder.SetDueDate(dueDate);
            _dueDateBuffer.Update(workOrder.WorkOrderId, dueDate);
        }

        if (workOrder.PrerequisiteOperations.Count > 0)
        {
            foreach (var prereqWorkOrder in workOrder.PrerequisiteOperations)
            {
                await SetWorkOrderTime(prereqWorkOrder, workOrder.DueDate - workOrder.Duration);
            }
        }
    }
}
