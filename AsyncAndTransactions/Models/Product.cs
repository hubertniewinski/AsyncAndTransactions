using System.ComponentModel.DataAnnotations;

namespace AsyncAndTransactions.Models;

public class Product(int idProduct, string name, string description, decimal price)
{
    public int IdProduct { get; init; } = idProduct;

    [MaxLength(200)]
    public string Name { get; init; } = name;

    [MaxLength(200)]
    public string Description { get; init; } = description;

    [DataType(DataType.Currency)]
    public decimal Price { get; init; } = price;
}