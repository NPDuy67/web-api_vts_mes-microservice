using MesMicroservice.Api.Application.Commands.MaterialDefinitions.MaterialUnits;
using MesMicroservice.Api.Application.Exceptions;
using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Api.Application.Commands.MaterialDefinitions.Operations;

public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand, bool>
{
    private readonly IMaterialDefinitionRepository _materialDefinitionRepository;

    public DeleteOperationCommandHandler(IMaterialDefinitionRepository materialDefinitionRepository)
    {
        _materialDefinitionRepository = materialDefinitionRepository;
    }

    public async Task<bool> Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
    {
        var materialDefinition = await _materialDefinitionRepository.GetAsync(request.MaterialDefinitionId) ?? throw new ResourceNotFoundException(nameof(MaterialDefinition), request.MaterialDefinitionId);

        await _materialDefinitionRepository.DeleteOperationAsync(request.OperationId);
        materialDefinition.RemoveOperation(request.OperationId);

        return await _materialDefinitionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
