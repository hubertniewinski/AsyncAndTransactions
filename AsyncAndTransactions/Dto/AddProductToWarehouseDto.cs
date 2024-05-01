using System.ComponentModel.DataAnnotations;

namespace AsyncAndTransactions.Dto;

public class AddProductToWarehouseDto
{
    [Required]
    public int IdProduct { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    [Required, Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be greater than zero.")]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}