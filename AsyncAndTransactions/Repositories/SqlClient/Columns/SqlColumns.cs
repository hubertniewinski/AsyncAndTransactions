using AsyncAndTransactions.Models;

namespace AsyncAndTransactions.Repositories.SqlClient.Columns;

internal static class SqlColumns
{
    internal static readonly SqlColumn<Warehouse> Warehouse_Id = Create<Warehouse>(nameof(Warehouse.IdWarehouse), p => p.IdWarehouse);
    
    internal static readonly SqlColumn<Order> Order_Id = Create<Order>(nameof(Order.IdOrder), p => p.IdOrder);
    internal static readonly SqlColumn<Order> Order_IdProduct = Create<Order>(nameof(Order.IdProduct), p => p.IdProduct);
    internal static readonly SqlColumn<Order> Order_Amount = Create<Order>(nameof(Order.Amount), p => p.Amount);
    internal static readonly SqlColumn<Order> Order_CreatedAt = Create<Order>(nameof(Order.CreatedAt), p => p.CreatedAt);
    internal static readonly SqlColumn<Order> Order_FulfilledAt = Create<Order>(nameof(Order.FulfilledAt), p => p.FulfilledAt);
    
    public static SqlColumn<T> Create<T>(string name, Func<T, object?> getDomainModelValue)
        => new(name, name.AsParameter(), getDomainModelValue);
}