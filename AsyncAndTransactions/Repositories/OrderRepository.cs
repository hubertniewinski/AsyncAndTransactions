using System.ComponentModel.DataAnnotations;
using AsyncAndTransactions.Models;
using AsyncAndTransactions.Repositories.Abstractions;
using AsyncAndTransactions.Repositories.SqlClient;
using AsyncAndTransactions.Repositories.SqlClient.Columns;
using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories;

internal class OrderRepository : BaseRepository, IOrderRepository
{
    protected override string TableName => $"[{nameof(Order)}]";
    
    public async Task<int> TryGetMatchingOfferIdAsync(SqlCommand command, int idProduct, int amount, DateTime productCreatedDate, CancellationToken cancellationToken)
    {
        var productCreatedDateParam = "@ProductCreatedDate";
        var whereSql = $"{SqlColumns.Order_IdProduct.AsEqualsQuery()} AND " +
                       $"{SqlColumns.Order_Amount.AsEqualsQuery()} AND " +
                       $"{SqlColumns.Order_CreatedAt.Name} < {productCreatedDateParam} AND " +
                       $"{SqlColumns.Order_FulfilledAt.Name} IS NULL";
        
        return await FirstOrDefaultAsync(command, whereSql, @params =>
        {
            @params.AddWithValue(SqlColumns.Order_IdProduct.Parameter, idProduct);
            @params.AddWithValue(SqlColumns.Order_Amount.Parameter, amount);
            @params.AddWithValue(productCreatedDateParam, productCreatedDate);
        }, r => r.Reader.GetInt32(r.Columns[SqlColumns.Order_Id.Name]), -1, cancellationToken);
    }

    public async Task FulfillAsync(SqlCommand command, int idOrder, DateTime date, CancellationToken cancellationToken)
    {
        var query = $"UPDATE {TableName} SET {SqlColumns.Order_FulfilledAt.AsEqualsQuery()} " +
                    $"WHERE {SqlColumns.Order_Id.AsEqualsQuery()}";
        
        var rowsAffected = await ExecuteNonQueryAsync(command, query, @params =>
        {
            @params.AddWithValue(SqlColumns.Order_FulfilledAt.Parameter, date);
            @params.AddWithValue(SqlColumns.Order_Id.Parameter, idOrder);
        }, cancellationToken);
        
        if(rowsAffected == 0)
        {
            throw new ValidationException($"Order with id {idOrder} not found");
        }
    }
}