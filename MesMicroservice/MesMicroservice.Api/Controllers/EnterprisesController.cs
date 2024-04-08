using MesMicroservice.Api.Application.Commands.Enterprises;
using MesMicroservice.Api.Application.Commands.Enterprises.Areas;
using MesMicroservice.Api.Application.Commands.Enterprises.Sites;
using MesMicroservice.Api.Application.Commands.Enterprises.WorkCenters;
using MesMicroservice.Api.Application.Commands.Enterprises.WorkUnits;
using MesMicroservice.Api.Application.Queries.Enterprises;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EnterprisesController : ApiControllerBase
{
    public EnterprisesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateEnterprise([FromBody] CreateEnterpriseCommand command)
    {
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{enterpriseId}")]
    public async Task<IActionResult> UpdateEnterprise([FromRoute] string enterpriseId, [FromBody] UpdateEnterpriseViewModel enterprise)
    {
        var command = new UpdateEnterpriseCommand(enterpriseId, enterprise.EnterpriseId, enterprise.Name);
        return await CommandAsync(command);
    }

    [HttpGet]
    public async Task<QueryResult<EnterpriseViewModel>> GetEnterprises([FromQuery] EnterprisesQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{enterpriseId}")]
    public async Task<IActionResult> DeteteEnterprise([FromRoute] string enterpriseId)
    {
        var command = new DeleteEnterpriseCommand(enterpriseId);
        return await CommandAsync(command);
    }

    [HttpPost]
    [Route("{enterpriseId}")]
    public async Task<IActionResult> CreateSite([FromRoute] string enterpriseId, [FromBody] CreateSiteViewModel site)
    {
        var command = new CreateSiteCommand(enterpriseId, site.SiteId, site.Name);
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{enterpriseId}/{siteId}")]
    public async Task<IActionResult> UpdateSite([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromBody] UpdateSiteViewModel site)
    {
        var command = new UpdateSiteCommand(enterpriseId, siteId, site.SiteId, site.Name);
        return await CommandAsync(command);
    }

    [HttpDelete]
    [Route("{enterpriseId}/{siteId}")]
    public async Task<IActionResult> DeleteSite([FromRoute] string enterpriseId, [FromRoute] string siteId)
    {
        var command = new DeleteSiteCommand(enterpriseId, siteId); 
        return await CommandAsync(command);
    }

    [HttpPost]
    [Route("{enterpriseId}/{siteId}")]
    public async Task<IActionResult> CreateArea([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromBody] CreateAreaViewModel area)
    {
        var command = new CreateAreaCommand(enterpriseId, siteId, area.AreaId, area.Name);
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{enterpriseId}/{siteId}/{areaId}")]
    public async Task<IActionResult> UpdateArea([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromBody] UpdateAreaViewModel area)
    {
        var command = new UpdateAreaCommand(enterpriseId, siteId, areaId, area.AreaId, area.Name);
        return await CommandAsync(command);
    }

    [HttpDelete]
    [Route("{enterpriseId}/{siteId}/{areaId}")]
    public async Task<IActionResult> DeleteArea([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId)
    {
        var command = new DeleteAreaCommand(enterpriseId, siteId, areaId);
        return await CommandAsync(command);
    }

    [HttpPost]
    [Route("{enterpriseId}/{siteId}/{areaId}")]
    public async Task<IActionResult> CreateWorkCenter([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromBody] CreateWorkCenterViewModel workCenter)
    {
        var command = new CreateWorkCenterCommand(enterpriseId, siteId, areaId, workCenter.WorkCenterId, workCenter.Name, workCenter.WorkCenterType);
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{enterpriseId}/{siteId}/{areaId}/{workCenterId}")]
    public async Task<IActionResult> UpdateWorkCenter([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromRoute] string workCenterId, [FromBody] UpdateWorkCenterViewModel workCenter)
    {
        var command = new UpdateWorkCenterCommand(enterpriseId, siteId, areaId, workCenterId, workCenter.WorkCenterId, workCenter.Name, workCenter.WorkCenterType);
        return await CommandAsync(command);
    }

    [HttpDelete]
    [Route("{enterpriseId}/{siteId}/{areaId}/{workCenterId}")]
    public async Task<IActionResult> DeleteWorkCenter([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromRoute] string workCenterId)
    {
        var command = new DeleteWorkCenterCommand(enterpriseId, siteId, areaId, workCenterId);
        return await CommandAsync(command);
    }

    [HttpPost]
    [Route("{enterpriseId}/{siteId}/{areaId}/{workCenterId}")]
    public async Task<IActionResult> CreateWorkUnit([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromRoute] string workCenterId, [FromBody] CreateWorkUnitViewModel workUnit)
    {
        var command = new CreateWorkUnitCommand(enterpriseId, siteId, areaId, workCenterId, workUnit.WorkUnitId, workUnit.Name);
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{enterpriseId}/{siteId}/{areaId}/{workCenterId}/{workUnitId}")]
    public async Task<IActionResult> UpdateWokUnit([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromRoute] string workCenterId, [FromRoute] string workUnitId, [FromBody] UpdateWorkUnitViewModel workUnit)
    {
        var command = new UpdateWorkUnitCommand(enterpriseId, siteId, areaId, workCenterId, workUnitId, workUnit.WorkUnitId, workUnit.Name);
        return await CommandAsync(command);
    }

    [HttpDelete]
    [Route("{enterpriseId}/{siteId}/{areaId}/{workCenterId}/{workUnitId}")]
    public async Task<IActionResult> DeleteWorkUnit([FromRoute] string enterpriseId, [FromRoute] string siteId, [FromRoute] string areaId, [FromRoute] string workCenterId, [FromRoute] string workUnitId)
    {
        var command = new DeleteWorkUnitCommand(enterpriseId, siteId, areaId, workCenterId, workUnitId);
        return await CommandAsync(command);
    }

    [HttpGet]
    [Route("Sites/Areas/WorkCenters/WorkUnit")]
    public async Task<QueryResult<WorkUnitViewModel>> GetWorkUnits([FromQuery] WorkUnitsQuery query)
    {
        return await _mediator.Send(query);
    }
}
