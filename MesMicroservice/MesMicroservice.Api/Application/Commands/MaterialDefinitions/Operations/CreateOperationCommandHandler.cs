using MesMicroservice.Api.Application.Exceptions;
using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Api.Application.Commands.MaterialDefinitions.Operations;

public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, bool>
{
    private readonly IMaterialDefinitionRepository _materialDefinitionRepository;
    private readonly IEquipmentClassRepository _equipmentClassRepository;

    public CreateOperationCommandHandler(IMaterialDefinitionRepository materialDefinitionRepository, IEquipmentClassRepository equipmentClassRepository)
    {
        _materialDefinitionRepository = materialDefinitionRepository;
        _equipmentClassRepository = equipmentClassRepository;
    }

    public async Task<bool> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
    {
        var materialDefinition = await _materialDefinitionRepository.GetAsync(request.MaterialDefinitionId) ?? throw new ResourceNotFoundException(nameof(MaterialDefinition), request.MaterialDefinitionId);
        var equipmentRequirements = new List<OperationEquipmentRequirement>();
        foreach (var equipmentRequirement in request.EquipmentRequirements)
        {
            var equipmentClass = await _equipmentClassRepository.GetAsync(equipmentRequirement.EquipmentClassId) ?? throw new ResourceNotFoundException(nameof(EquipmentClass), equipmentRequirement.EquipmentClassId);

            equipmentRequirements.Add(new OperationEquipmentRequirement(equipmentClass, equipmentRequirement.Quantity));
        }

        materialDefinition.AddOperation(request.OperationId, request.Name, request.PrerequisiteOperation, equipmentRequirements);
        return await _materialDefinitionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
