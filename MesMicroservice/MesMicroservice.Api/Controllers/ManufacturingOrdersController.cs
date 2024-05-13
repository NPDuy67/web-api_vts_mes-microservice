using MesMicroservice.Api.Application.Commands.ManufacturingOrders;
using MesMicroservice.Api.Application.Queries.ManufacturingOrders;
using MesMicroservice.Api.Application.Queries.WorkOrders;
using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ManufacturingOrdersController : ApiControllerBase
{
    public ManufacturingOrdersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateManufacturingOrder([FromBody] CreateManufacturingOrderCommand command)
    {
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{manufacturingOrderId}")]
    public async Task<IActionResult> UpdateManufacturingOrder([FromRoute] string manufacturingOrderId, [FromBody] UpdateManufacturingOrderViewModel manufacturingOrder)
    {
        var command = new UpdateManufacturingOrderCommand(manufacturingOrderId, manufacturingOrder.MaterialDefinitionId, manufacturingOrder.Quantity, manufacturingOrder.Unit, manufacturingOrder.DueDate, manufacturingOrder.AvailableDate, manufacturingOrder.Priority);
        return await CommandAsync(command);
    }

    [HttpGet]
    public async Task<QueryResult<ManufacturingOrderViewModel>> GetManufacturingOrders([FromQuery] ManufacturingOrdersQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{manufacturingOrderId}")]
    public async Task<IActionResult> DeleteManufacturingOrder([FromRoute] string manufacturingOrderId)
    {
        var command = new DeleteManufacturingOrderCommand(manufacturingOrderId);
        return await CommandAsync(command);
    }

    [HttpGet]
    [Route("{manufacturingOrderId}")]
    public async Task<IActionResult> GetManufacturingOrder([FromRoute] string manufacturingOrderId)
    {
        var query = new ManufacturingOrderQuery(manufacturingOrderId);
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
}
