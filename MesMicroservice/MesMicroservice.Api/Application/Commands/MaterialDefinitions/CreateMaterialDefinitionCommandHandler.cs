using MesMicroservice.Api.Application.Exceptions;
using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Api.Application.Commands.MaterialDefinitions;

public class CreateMaterialDefinitionCommandHandler : IRequestHandler<CreateMaterialDefinitionCommand, bool>
{
    private readonly IMaterialDefinitionRepository _materialDefinitionRepository;
    private readonly IMaterialClassRepository _materialClassRepository;

    public CreateMaterialDefinitionCommandHandler(IMaterialDefinitionRepository materialDefinitionRepository, IMaterialClassRepository materialClassRepository)
    {
        _materialDefinitionRepository = materialDefinitionRepository;
        _materialClassRepository = materialClassRepository;
    }

    public async Task<bool> Handle(CreateMaterialDefinitionCommand request, CancellationToken cancellationToken)
    {
        var properties = request.Properties.Select(x => new MaterialDefinitionProperty(
            x.PropertyId,
            x.Description,
            new Value(x.ValueString, x.ValueType),
            x.ValueUnitOfMeasure))
            .ToList();

        var materialClass = await _materialClassRepository.GetAsync(request.MaterialClass) ?? throw new ResourceNotFoundException(nameof(MaterialClass), request.MaterialClass);
        var materialDefinition = new MaterialDefinition(request.MaterialDefinitionId, request.Name, request.PrimaryUnit, properties, materialClass);

        await _materialDefinitionRepository.Add(materialDefinition);

        return await _materialDefinitionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
