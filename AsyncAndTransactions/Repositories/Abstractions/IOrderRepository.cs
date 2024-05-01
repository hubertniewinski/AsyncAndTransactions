using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories.Abstractions;

public interface IOrderRepository
{
    public Task<int> TryGetMatchingOfferIdAsync(SqlCommand command, int idProduct, int amount, DateTime productCreatedDate, CancellationToken cancellationToken = default);
    public Task FulfillAsync(SqlCommand command, int idOrder, DateTime date, CancellationToken cancellationToken = default);
}