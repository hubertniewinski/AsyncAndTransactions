using AsyncAndTransactions.Models;
using AsyncAndTransactions.Repositories.Abstractions;
using AsyncAndTransactions.Repositories.SqlClient;
using AsyncAndTransactions.Repositories.SqlClient.Columns;
using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories;

internal class WarehouseRepository : BaseRepository, IWarehouseRepository
{
    protected override string TableName => nameof(Warehouse);
    
    public Task<bool> ExistsAsync(SqlCommand command, int id, CancellationToken cancellationToken)
    {
        var whereSql = SqlColumns.Warehouse_Id.AsEqualsQuery();
        return ExistsAsync(command, whereSql, @params => @params.AddWithValue(SqlColumns.Warehouse_Id.Parameter, id), cancellationToken);
    }
}