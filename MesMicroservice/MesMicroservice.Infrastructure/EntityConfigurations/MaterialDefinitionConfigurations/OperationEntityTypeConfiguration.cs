using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations.MaterialDefinitionConfigurations;
public class OperationEntityTypeConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .UseHiLo("operationeq", ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasIndex(x => new { x.OperationId , x.MaterialDefinitionId}).IsUnique();
        builder.Property(x => x.OperationId).HasMaxLength(50).IsRequired();

        builder.HasMany(x => x.PrerequisiteOperation).WithMany();

        builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
    }
}
