using AsyncAndTransactions.Models;
using AsyncAndTransactions.Repositories.Abstractions;
using AsyncAndTransactions.Repositories.SqlClient;
using AsyncAndTransactions.Repositories.SqlClient.Columns;
using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories;

internal class ProductWarehouseRepository : BaseRepository, IProductWarehouseRepository
{
    protected override string TableName => "Product_Warehouse";
    
    public Task<bool> ExistsByIdOrderAsync(SqlCommand command, int idOrder, CancellationToken cancellationToken)
    {
        var whereSql = ProductWarehouseSqlColumns.IdOrder.AsEqualsQuery();
        return ExistsAsync(command, whereSql, @params => @params.AddWithValue(ProductWarehouseSqlColumns.IdOrder.Parameter, idOrder), cancellationToken);
    }

    public async Task<int> AddAsync(SqlCommand command, ProductWarehouse productWarehouse, CancellationToken cancellationToken)
    {
        var query = $"INSERT INTO {TableName} {ProductWarehouseSqlColumns.CreateColumns.AsInsertQuery($"OUTPUT INSERTED.{ProductWarehouseSqlColumns.IdProductWarehouse.Name}")}";
        var result = await ExecuteScalarAsync(command, query, @params => @params.AddWithValue(ProductWarehouseSqlColumns.CreateColumns, productWarehouse), cancellationToken);
        return (int)result!;
    }
}