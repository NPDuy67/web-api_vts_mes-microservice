using Microsoft.EntityFrameworkCore.Storage;
using MesMicroservice.Domain.SeedWork;
using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;
using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;
using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;
using MesMicroservice.Infrastructure.EntityConfigurations;
using MesMicroservice.Infrastructure.EntityConfigurations.HierarchyModelConfigurations;
using MesMicroservice.Infrastructure.EntityConfigurations.MaterialDefinitionConfigurations;
using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
using MesMicroservice.Domain.AggregateModels.WorkCenterOutputAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialLotAggregate;
using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;
using MesMicroservice.Domain.AggregateModels.ResourceRelationshipNetworkAggregate;
using MesMicroservice.Domain.AggregateModels;
using MesMicroservice.Domain.AggregateModels.ManufacucturingRecordAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;

namespace MesMicroservice.Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "application";

    private IDbContextTransaction? _currentTransaction;
    private readonly IMediator _mediator;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ApplicationDbContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public DbSet<Enterprise> Enterprises { get; set; }
    public DbSet<EquipmentClass> EquipmentClasses { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<WorkCenterOutput> WorkCenterOutputs { get; set; }
    public DbSet<ManufacturingOrder> ManufacturingOrders { get; set; }
    public DbSet<WorkOrder> WorkOrders { get; set; }
    public DbSet<MaterialClass> MaterialClasses { get; set; }
    public DbSet<MaterialDefinition> MaterialDefinitions { get; set; }
    public DbSet<MaterialLot> MaterialLots { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<ResourceRelationshipNetwork> ResourceRelationshipNetworks { get; set; }
    public DbSet<ManufacturingRecord> ManufacturingRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ResourceEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new ResourceRelationshipNetworkEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new HierarchyModelEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EnterpriseEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SiteEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AreaEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WorkCenterEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WorkUnitEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new WorkCenterOutputEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new EquipmentClassEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new EquipmentEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new ManufacturingOrderEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new WorkOrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentWorkOrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WorkOrderEquipmentRequirementEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new MaterialClassEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new MaterialDefinitionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MaterialUnitEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperationEquipmentRequirementEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new MaterialLotEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new ManufacturingRecordEntityTypeConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync();

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}