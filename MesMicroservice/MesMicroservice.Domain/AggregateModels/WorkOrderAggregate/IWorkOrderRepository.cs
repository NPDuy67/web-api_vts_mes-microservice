namespace MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

public interface IWorkOrderRepository : IRepository<WorkOrder>
{
    public Task<WorkOrder> Add(int manufacturingOrderId, WorkOrder workOrder);
    public Task<WorkOrder?> GetAsync(int manufacturingOrderId, string workOrderId);
    public Task<List<WorkOrder>> GetListByIdAsync(int manufacturingOrderId, List<string> workOrderIds);
    public WorkOrder Update(WorkOrder workOrder);
    public Task<bool> ExistsAsync(int manufacturingOrderId, string workOrderId);
    public Task DeleteAsync(int manufacturingOrderId, string workOrderId);
}
