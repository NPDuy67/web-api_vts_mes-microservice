using MesMicroservice.Api.Application.Exceptions;
using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;

namespace MesMicroservice.Api.Application.Commands.Enterprises.Sites;

public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand, bool>
{
    private readonly IEnterpriseRepository _enterpriseRepository;

    public CreateSiteCommandHandler(IEnterpriseRepository enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;
    }

    public async Task<bool> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        var enterprise = await _enterpriseRepository.GetAsync(request.EnterpriseId) ?? throw new ResourceNotFoundException(nameof(Enterprise), request.EnterpriseId);

        var site = new Site(request.SiteId, request.Name);

        enterprise.AddSite(site);

        return await _enterpriseRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
