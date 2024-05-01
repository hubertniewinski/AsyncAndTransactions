using AsyncAndTransactions.Models;
using AsyncAndTransactions.Repositories.Abstractions;
using AsyncAndTransactions.Repositories.SqlClient;
using AsyncAndTransactions.Repositories.SqlClient.Columns;
using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories;

internal class ProductRepository : BaseRepository, IProductRepository
{
    protected override string TableName => nameof(Product);

    public Task<Product?> GetByIdAsync(SqlCommand command, int id, CancellationToken cancellationToken)
    {
        var whereSql = ProductSqlColumns.IdProduct.AsEqualsQuery();
        return FirstOrDefaultAsync(command, whereSql, @params => @params.AddWithValue(ProductSqlColumns.IdProduct.Parameter, id), 
            ProductSqlColumns.RetrieveDomainModel, cancellationToken: cancellationToken);
    }
}