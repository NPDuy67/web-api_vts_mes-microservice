namespace MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;

public interface IManufacturingOrderRepository : IRepository<ManufacturingOrder>
{
    public Task<ManufacturingOrder> Add(ManufacturingOrder manufacturingOrder);
    public Task<ManufacturingOrder?> GetAsync(string manufacturingOrderId);
    public Task<List<ManufacturingOrder>> GetListByIdAsync(List<string> manufacturingOrderIds);
    public ManufacturingOrder Update(ManufacturingOrder manufacturingOrder);
    public Task<bool> ExistsAsync(string manufacturingOrderId);
    public Task DeleteAsync(string manufacturingOrderId);
}
