namespace AsyncAndTransactions.Models;

public class Order(int idOrder, int idProduct, int amount, DateTime createdAt, DateTime? fulfilledAt)
{
    public int IdOrder { get; init; } = idOrder;
    public int IdProduct { get; init; } = idProduct;
    public int Amount { get; init; } = amount;
    public DateTime CreatedAt { get; init; } = createdAt;
    public DateTime? FulfilledAt { get; init; } = fulfilledAt;
}