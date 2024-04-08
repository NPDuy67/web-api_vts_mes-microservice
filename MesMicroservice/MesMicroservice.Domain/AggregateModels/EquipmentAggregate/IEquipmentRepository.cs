namespace MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
public interface IEquipmentRepository: IRepository<Equipment>
{
    public Task<Equipment> Add(Equipment equipment);
    public Task<Equipment?> GetAsync(string equipmentId);
    public Task<List<Equipment>> GetListByIdAsync(List<string> equipmentIds);
    public Equipment Update(Equipment equipment);
    public Task<bool> ExistsAsync(string equipmentId);
    public Task DeleteAsync(string equipmentId);
}
