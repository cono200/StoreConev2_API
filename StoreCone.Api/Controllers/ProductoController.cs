using Drivers.Api.Models;
using Microsoft.AspNetCore.Mvc;
using StoreCone.Api.Models;
using StoreCone.Api.Services;

namespace StoreCone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ILogger<ProductoController> _logger;
    private readonly ProductoServices _productoServices;
    public ProductoController(
        ILogger<ProductoController> logger,
        ProductoServices productoServices)
    {
        _logger = logger;
        _productoServices = productoServices;
    }
    //MOSTRAR
    [HttpGet("Listar")]
    public async Task<IActionResult> GetProducto()
    {
        var producto = await _productoServices.GetAsync();
        return Ok(producto);
    }


    //INSERTAR
    [HttpPost("Crear")]
    public async Task<IActionResult> CreateDriver([FromBody] Producto producto)
    {
        if (producto == null)
            return BadRequest();
        if (producto.Nombre == string.Empty)
            ModelState.AddModelError("Nombre", "El producto no debe estar vacio");

        await _productoServices.InsertarProducto(producto);

        return Created("Created", true);
    }

    //ELIMINAR
    [HttpDelete("Borrar/{Id}")]
    public async Task<IActionResult> BorrarProducto(string Id)
    {
        await _productoServices.BorrarProducto(Id);
        return NoContent();
    }

    //EDITAR
    [HttpPut("Editar")]
    public async Task<IActionResult> EditarProducto([FromBody] Producto producto, string Id)
    {
        if (producto == null)
            return BadRequest();
        if (producto.Nombre == string.Empty)
            ModelState.AddModelError("Nombre", "El producto no debe estar vacio");
        producto.Id = Id;

        await _productoServices.EditarProducto(producto);
        return Created("Created", true);
    }

    //BUSCAR POR ID
    [HttpGet("Buscar/{Id}")]
    public async Task<IActionResult> ProductoPorId(string Id)
    {
        return Ok(await _productoServices.ProductoPorId(Id));
    }
}

