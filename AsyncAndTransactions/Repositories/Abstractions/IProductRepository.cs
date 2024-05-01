using AsyncAndTransactions.Models;
using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories.Abstractions;

public interface IProductRepository
{
    public Task<Product?> GetByIdAsync(SqlCommand command, int id, CancellationToken cancellationToken = default);
}