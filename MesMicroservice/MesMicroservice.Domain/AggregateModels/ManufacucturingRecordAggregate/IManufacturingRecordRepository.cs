namespace MesMicroservice.Domain.AggregateModels.ManufacucturingRecordAggregate;
public interface IManufacturingRecordRepository: IRepository<ManufacturingRecord>
{
    ManufacturingRecord Add(ManufacturingRecord record);
}
