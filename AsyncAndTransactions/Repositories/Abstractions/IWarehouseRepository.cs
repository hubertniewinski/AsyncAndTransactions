using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories.Abstractions;

public interface IWarehouseRepository
{
    public Task<bool> ExistsAsync(SqlCommand command, int id, CancellationToken cancellationToken = default);
}