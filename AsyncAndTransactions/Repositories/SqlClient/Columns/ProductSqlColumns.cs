using AsyncAndTransactions.Models;

namespace AsyncAndTransactions.Repositories.SqlClient.Columns;

internal static class ProductSqlColumns
{
    public static readonly SqlColumn<Product> IdProduct = SqlColumns.Create<Product>(nameof(Product.IdProduct), p => p.IdProduct);
    public static readonly SqlColumn<Product> Name = SqlColumns.Create<Product>(nameof(Product.Name), p => p.Name);
    public static readonly SqlColumn<Product> Description = SqlColumns.Create<Product>(nameof(Product.Description), p => p.Description);
    public static readonly SqlColumn<Product> Price = SqlColumns.Create<Product>(nameof(Product.Price), p => p.Price);
    
    public static Product RetrieveDomainModel(SqlReadRowModel r)
    {
        var idProduct = r.Reader.GetInt32(r.Columns[IdProduct.Name]);
        var name = r.Reader.GetString(r.Columns[Name.Name]);
        var description = r.Reader.GetString(r.Columns[Description.Name]);
        var price = r.Reader.GetDecimal(r.Columns[Price.Name]);

        return new(idProduct, name, description, price);
    }
}