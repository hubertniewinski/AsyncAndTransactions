using AsyncAndTransactions.Dto;

namespace AsyncAndTransactions.Services;

public interface IProductWarehouseService
{
    public Task<int> AddByProcedureAsync(AddProductToWarehouseDto dto, CancellationToken cancellationToken = default);
    public Task<int> AddAsync(AddProductToWarehouseDto dto, CancellationToken cancellationToken = default);
}