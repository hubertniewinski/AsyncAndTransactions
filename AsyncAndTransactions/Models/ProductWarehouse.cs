using System.ComponentModel.DataAnnotations;

namespace AsyncAndTransactions.Models;

public class ProductWarehouse(
    int idProductWarehouse,
    int idWarehouse,
    int idProduct,
    int idOrder,
    int amount,
    decimal price,
    DateTime createdAt)
{
    public int IdProductWarehouse { get; init; } = idProductWarehouse;
    public int IdWarehouse { get; init; } = idWarehouse;
    public int IdProduct { get; init; } = idProduct;
    public int IdOrder { get; init; } = idOrder;
    [DataType(DataType.Currency)]
    public int Amount { get; init; } = amount;
    public decimal Price { get; init; } = price;
    public DateTime CreatedAt { get; init; } = createdAt;
}