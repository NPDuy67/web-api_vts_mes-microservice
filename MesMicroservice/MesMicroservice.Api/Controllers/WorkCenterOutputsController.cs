using MesMicroservice.Api.Application.Commands.Enterprises.WorkCenters;
using MesMicroservice.Api.Application.Queries.WorkCenterOutputs;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkCenterOutputsController : ApiControllerBase
{
    public WorkCenterOutputsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("{enterpriseId}/{siteId}/{areaId}/{workCenterId}")]
    public async Task<IActionResult> CreateWorkCenterOutput([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromRoute] string workCenterId, [FromBody] CreateWorkCenterOutputViewModel workCenterOutput)
    {
        var command = new CreateWorkCenterOutputCommand($"{enterpriseId}/{siteId}/{areaId}/{workCenterId}", workCenterOutput.MaterialDefinitionId, workCenterOutput.Output, workCenterOutput.MaterialUnitId);
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{enterpriseId}/{siteId}/{areaId}/{workCenterId}/{materialDefinitionId}")]
    public async Task<IActionResult> UpdateWorkCenterOutput([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromRoute] string workCenterId, [FromRoute] string materialDefinitionId, [FromBody] UpdateWorkCenterOutputViewModel workCenterOutput)
    {
        var command = new UpdateWorkCenterOutputCommand($"{enterpriseId}/{siteId}/{areaId}/{workCenterId}", materialDefinitionId, workCenterOutput.Output, workCenterOutput.MaterialUnitId);
        return await CommandAsync(command);
    }

    [HttpGet]
    [Route("{enterpriseId}/{siteId}/{areaId}/{workCenterId}")]
    public async Task<IEnumerable<WorkCenterOutputViewModel>> GetWorkCenterOutputs([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromRoute] string workCenterId)
    {
        var query = new WorkCenterOutputsQuery($"{enterpriseId}/{siteId}/{areaId}/{workCenterId}");
        return await _mediator.Send(query);
    }

    [HttpGet]
    [Route("{manufacturingOrderId}")]
    public async Task<IEnumerable<WorkCenterOutputViewModel>> GetWorkCenterOutputByManufacturingOrderId([FromRoute] string manufacturingOrderId)
    {
        var query = new WorkCenterOutputsByManuOrderQuery(manufacturingOrderId);
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{enterpriseId}/{siteId}/{areaId}/{workCenterId}/{materialDefinitionId}")]
    public async Task<IActionResult> DeleteWorkCenterOutput([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromRoute] string workCenterId, [FromRoute] string materialDefinitionId)
    {
        var command = new DeleteWorkCenterOutputCommand($"{enterpriseId}/{siteId}/{areaId}/{workCenterId}", materialDefinitionId);
        return await CommandAsync(command);
    }
}
