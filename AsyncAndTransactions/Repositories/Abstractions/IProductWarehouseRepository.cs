using AsyncAndTransactions.Models;
using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories.Abstractions;

public interface IProductWarehouseRepository
{
    public Task<bool> ExistsByIdOrderAsync(SqlCommand command, int idOrder, CancellationToken cancellationToken = default);
    public Task<int> AddAsync(SqlCommand command, ProductWarehouse productWarehouse, CancellationToken cancellationToken = default);
}