using System.Data;
using AsyncAndTransactions.Dto;
using AsyncAndTransactions.Exceptions;
using AsyncAndTransactions.Models;
using AsyncAndTransactions.Repositories.Abstractions;
using AsyncAndTransactions.Repositories.SqlClient;
using AsyncAndTransactions.Repositories.SqlClient.Columns;
using Microsoft.Data.SqlClient;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace AsyncAndTransactions.Services;

internal class ProductWarehouseService(IProductRepository productRepository, 
    IWarehouseRepository warehouseRepository, IOrderRepository orderRepository,
    IProductWarehouseRepository productWarehouseRepository, IConfiguration configuration) : IProductWarehouseService
{
    private string ConnectionString => configuration.GetConnectionString("DefaultConnection")!;

    public async Task<int> AddByProcedureAsync(AddProductToWarehouseDto dto, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        await connection.OpenAsync(cancellationToken);
        
        var result = await BaseRepository.ExecuteScalarAsync(command, "AddProductToWarehouse", @params =>
        {
            @params.AddWithValue(ProductSqlColumns.IdProduct.Parameter, dto.IdProduct);
            @params.AddWithValue(SqlColumns.Warehouse_Id.Parameter, dto.IdWarehouse);
            @params.AddWithValue(SqlColumns.Order_Amount.Parameter, dto.Amount);
            @params.AddWithValue("@CreatedAt", dto.CreatedAt);
        }, cancellationToken);
        return (int)result;
    }

    public async Task<int> AddAsync(AddProductToWarehouseDto dto, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await using var command = connection.CreateCommand();
        await connection.OpenAsync(cancellationToken);
        var tran = await connection.BeginTransactionAsync(cancellationToken);
        command.Transaction = (SqlTransaction)tran;

        try
        {
            var product = await productRepository.GetByIdAsync(command, dto.IdProduct, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException($"Product with id {dto.IdProduct} not found");
            }

            await ValidateWarehouseExistsAsync(command, dto.IdWarehouse, cancellationToken);
            var orderId = await ValidateOfferMatchAsync(command, dto.IdProduct, dto.Amount, dto.CreatedAt, cancellationToken);
            await ValidateOrderInWarehouseAlreadyExistsAsync(command, orderId, cancellationToken);

            var currentDate = DateTime.UtcNow;
            await orderRepository.FulfillAsync(command, orderId, currentDate, cancellationToken);

            var price = product.Price * dto.Amount;
            var productWarehouse = new ProductWarehouse(default, dto.IdWarehouse, dto.IdProduct, orderId, dto.Amount,
                price, currentDate);
            var result = await productWarehouseRepository.AddAsync(command, productWarehouse, cancellationToken);
            await tran.CommitAsync(cancellationToken);
            return result;
        }
        catch (Exception _)
        {
            await tran.RollbackAsync(cancellationToken);
            throw;
        }
    }
    
    private async Task ValidateWarehouseExistsAsync(SqlCommand command, int idWarehouse, CancellationToken cancellationToken)
    {
        var warehouseExists = await warehouseRepository.ExistsAsync(command, idWarehouse, cancellationToken);
        
        if (!warehouseExists)
        {
            throw new NotFoundException($"Warehouse with id {idWarehouse} not found");
        }
    }
    
    private async Task<int> ValidateOfferMatchAsync(SqlCommand command, int idProduct, int amount, DateTime productCreatedDate, CancellationToken cancellationToken)
    {
        var orderId = await orderRepository.TryGetMatchingOfferIdAsync(command, idProduct, amount, productCreatedDate, cancellationToken);
        
        if (orderId == -1)
        {
            throw new NotFoundException($"Order with product id = {idProduct}, amount = {amount} " +
                                        $"and created date earlier than {productCreatedDate} not found or its already fulfilled");
        }

        return orderId;
    }
    
    private async Task ValidateOrderInWarehouseAlreadyExistsAsync(SqlCommand command, int orderId, CancellationToken cancellationToken)
    {
        var orderInWarehouseExists = await productWarehouseRepository.ExistsByIdOrderAsync(command, orderId, cancellationToken);
        
        if (orderInWarehouseExists)
        {
            throw new ValidationException($"Order with id {orderId} already exists in warehouse");
        }
    }
}