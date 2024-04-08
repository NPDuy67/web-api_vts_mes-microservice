using MesMicroservice.Domain.AggregateModels.ManufacucturingRecordAggregate;

namespace MesMicroservice.Infrastructure.Repositories;
public class ManufacturingOrderRecordRepository : BaseRepository, IManufacturingRecordRepository
{
    public ManufacturingOrderRecordRepository(ApplicationDbContext context) : base(context)
    {
    }

    public ManufacturingRecord Add(ManufacturingRecord record)
    {
        if (record.IsTransient())
        {
            return _context.ManufacturingRecords
                .Add(record)
                .Entity;
        }
        else
        {
            return record;
        }
    }
}
