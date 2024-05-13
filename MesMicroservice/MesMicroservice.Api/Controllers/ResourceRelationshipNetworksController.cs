using MesMicroservice.Api.Application.Commands.ResourceRelationshipNetworks;
using MesMicroservice.Api.Application.Commands.ResourceRelationshipNetworks.ResourceNetworkConnections;
using MesMicroservice.Api.Application.Queries.ResourceRelationshipNetworks;
using MesMicroservice.Api.Application.Queries.ResourceRelationshipNetworks.ResourceNetworkConnections;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResourceRelationshipNetworksController : ApiControllerBase
{
    public ResourceRelationshipNetworksController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateResourceRelationshipNetwork([FromBody] CreateResourceRelationshipNetworkCommand command)
    {
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{resourceRelationshipNetworkId}")]
    public async Task<IActionResult> UpdateResourceRelationshipNetwork(string resourceRelationshipNetworkId, [FromBody] UpdateResourceRelationshipNetworkViewModel relationship)
    {
        var command = new UpdateResourceRelationshipNetworkCommand(resourceRelationshipNetworkId, relationship.Description, relationship.RelationshipType, relationship.RelationshipForm);
        return await CommandAsync(command);
    }

    [HttpGet]
    public async Task<QueryResult<ResourceRelationshipNetworkViewModel>> GetResourceRelationshipNetworks([FromQuery] ResourceRelationshipNetworksQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{resourceRelationshipNetworkId}")]
    public async Task<IActionResult> DeleteResourceRelationshipNetwork([FromRoute] string resourceRelationshipNetworkId)
    {
        var command = new DeleteResourceRelationshipNetworkCommand(resourceRelationshipNetworkId);
        return await CommandAsync(command);
    }

    [HttpPost]
    [Route("{resourceRelationshipNetworkId}/Connections")]
    public async Task<IActionResult> CreateConnection([FromRoute] string resourceRelationshipNetworkId, [FromBody] CreateResourceNetworkConnectionViewModel connection)
    {
        var command = new CreateResourceNetworkConnectionCommand(resourceRelationshipNetworkId, connection.ConnectionId, connection.Description, connection.Properties, connection.FromResource, connection.ToResource);
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{resourceRelationshipNetworkId}/Connections/{connectionId}")]
    public async Task<IActionResult> UpdateConnection([FromRoute] string resourceRelationshipNetworkId, [FromRoute] string connectionId, [FromBody] UpdateResourceNetworkConnectionViewModel connection)
    {
        var command = new UpdateResourceNetworkConnectionCommand(resourceRelationshipNetworkId, connectionId, connection.Description, connection.Properties, connection.FromResource, connection.ToResource);
        return await CommandAsync(command);
    }

    [HttpDelete]
    [Route("{resourceRelationshipNetworkId}/Connections/{connectionId}")]
    public async Task<IActionResult> DeleteConnection([FromRoute] string resourceRelationshipNetworkId, [FromRoute] string connectionId)
    {
        var command = new DeleteResourceNetworkConnectionCommand(resourceRelationshipNetworkId, connectionId);
        return await CommandAsync(command);
    }

    [HttpDelete]
    [Route("{resourceRelationshipNetworkId}/Connections")]
    public async Task<IActionResult> DeleteConnectionsByResourceId([FromRoute] string resourceRelationshipNetworkId, string resourceId)
    {
        var command = new DeleteConnectionsByResourceIdCommand(resourceRelationshipNetworkId, resourceId);
        return await CommandAsync(command);
    }

    [HttpGet]
    [Route("{resourceRelationshipNetworkId}/Connections")]
    public async Task<QueryResult<ResourceNetworkConnectionViewModel>> GetResourceRelationshipNetworkConnections([FromQuery] ResourceRelationshipConnectionsQuery query, [FromRoute] string resourceRelationshipNetworkId)
    {
        query.NetworkId = resourceRelationshipNetworkId;
        return await _mediator.Send(query);
    }
}
