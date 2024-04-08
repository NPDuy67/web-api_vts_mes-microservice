using MesMicroservice.Api.Application.Commands.WorkOrders;
using MesMicroservice.Api.Application.Queries.WorkOrders;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WorkOrdersController : ApiControllerBase
{
    public WorkOrdersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("{manufacturingOrderId}")]
    public async Task<IActionResult> CreateWorkOrder([FromRoute] string manufacturingOrderId, [FromBody] CreateWorkOrderViewModel workOrder)
    {
        var command = new CreateWorkOrderCommand(manufacturingOrderId, workOrder.WorkOrderId, workOrder.DueDate, workOrder.StartTime, workOrder.EndTime, workOrder.WorkOrderStatus, workOrder.PrerequisiteOperations, workOrder.WorkCenter, workOrder.EquipmentRequirements);
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{manufacturingOrderId}/{workOrderId}")]
    public async Task<IActionResult> UpdateWorkOrder([FromRoute] string manufacturingOrderId, [FromRoute] string workOrderId, [FromBody] UpdateWorkOrderViewModel workOrder)
    {
        var command = new UpdateWorkOrderCommand(manufacturingOrderId, workOrderId, workOrder.StartTime, workOrder.EndTime, workOrder.ActuallyStartTime, workOrder.ActuallyEndTime, workOrder.WorkOrderStatus, workOrder.WorkCenter);
        return await CommandAsync(command);
    }

    [HttpGet]
    public async Task<QueryResult<WorkOrderViewModel>> GetWorkOrders([FromQuery] WorkOrdersQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet]
    [Route("{manufacturingOrderId}/{workOrderId}")]
    public async Task<IActionResult> GetWorkOrder([FromRoute] string manufacturingOrderId, [FromRoute] string workOrderId)
    {
        var query = new WorkOrderQuery(manufacturingOrderId, workOrderId);
        var result = await _mediator.Send(query);

        if (result is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpDelete]
    [Route("{manufacturingOrderId}/{workOrderId}")]
    public async Task<IActionResult> DeleteWorkOrder([FromRoute] string manufacturingOrderId, [FromRoute] string workOrderId)
    {
        var command = new DeleteWorkOrderCommand(manufacturingOrderId, workOrderId);
        return await CommandAsync(command);
    }
}
