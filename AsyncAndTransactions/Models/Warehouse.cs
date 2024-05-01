using System.ComponentModel.DataAnnotations;

namespace AsyncAndTransactions.Models;

public class Warehouse(int idWarehouse, string name, string address)
{
    public int IdWarehouse { get; init; } = idWarehouse;

    [MaxLength(200)]
    public string Name { get; init; } = name;

    [MaxLength(200)]
    public string Address { get; init; } = address;
}