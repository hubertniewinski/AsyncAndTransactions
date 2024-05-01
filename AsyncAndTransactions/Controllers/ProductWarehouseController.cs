using AsyncAndTransactions.Dto;
using AsyncAndTransactions.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsyncAndTransactions.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductWarehouseController(IProductWarehouseService productWarehouseService) : ControllerBase
{
    [HttpPost]
    [ActionName("AddProductToWarehouseAsync")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProductToWarehouseAsync([FromBody] AddProductToWarehouseDto dto, CancellationToken cancellationToken)
    {
        var id = await productWarehouseService.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(AddProductToWarehouseAsync), new {id});
    }
    
    [HttpPost("byProcedure")]
    [ActionName("AddProductToWarehouseByProcedureAsync")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProductToWarehouseByProcedureAsync([FromBody] AddProductToWarehouseDto dto, CancellationToken cancellationToken)
    {
        var id = await productWarehouseService.AddByProcedureAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(AddProductToWarehouseByProcedureAsync), new {id});
    }
}