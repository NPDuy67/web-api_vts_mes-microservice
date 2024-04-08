using MesMicroservice.Api.Application.Exceptions;
using MesMicroservice.Api.Application.Messages;
using MesMicroservice.Domain.Exceptions;
using MesMicroservice.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected readonly IMediator _mediator;

    public ApiControllerBase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<IActionResult> CommandAsync<T>(IRequest<T> request)
    {
        try
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        catch (ResourceNotFoundException ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return NotFound(errorMessage);
        }
        catch (EntitiesNotFoundException ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return BadRequest(errorMessage);
        }
        catch (WorkOrderNotScheduledException ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return BadRequest(errorMessage);
        }
        catch (EquipmentOccupiedException ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return BadRequest(errorMessage);
        }
        catch (EntityDuplicationException ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return BadRequest(errorMessage);
        }
        catch (Exception ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return BadRequest(errorMessage);
        }
    }
}
