using AsyncAndTransactions.Models;

namespace AsyncAndTransactions.Repositories.SqlClient.Columns;

internal static class ProductWarehouseSqlColumns
{
    public static readonly SqlColumn<ProductWarehouse> IdProductWarehouse = 
        SqlColumns.Create<ProductWarehouse>(nameof(ProductWarehouse.IdProductWarehouse), p => p.IdProductWarehouse);
    public static readonly SqlColumn<ProductWarehouse> IdWarehouse = 
        SqlColumns.Create<ProductWarehouse>(nameof(ProductWarehouse.IdWarehouse), p => p.IdWarehouse);
    public static readonly SqlColumn<ProductWarehouse> IdProduct = 
        SqlColumns.Create<ProductWarehouse>(nameof(ProductWarehouse.IdProduct), p => p.IdProduct);
    public static readonly SqlColumn<ProductWarehouse> IdOrder = 
        SqlColumns.Create<ProductWarehouse>(nameof(ProductWarehouse.IdOrder), p => p.IdOrder);
    public static readonly SqlColumn<ProductWarehouse> Amount = 
        SqlColumns.Create<ProductWarehouse>(nameof(ProductWarehouse.Amount), p => p.Amount);
    public static readonly SqlColumn<ProductWarehouse> Price = 
        SqlColumns.Create<ProductWarehouse>(nameof(ProductWarehouse.Price), p => p.Price);
    public static readonly SqlColumn<ProductWarehouse> CreatedAt = 
        SqlColumns.Create<ProductWarehouse>(nameof(ProductWarehouse.CreatedAt), p => p.CreatedAt);
    
    internal static readonly SqlColumn<ProductWarehouse>[] CreateColumns = [
        IdWarehouse,
        IdProduct,
        IdOrder,
        Amount,
        Price,
        CreatedAt
    ];
}