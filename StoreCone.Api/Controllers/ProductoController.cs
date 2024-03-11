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
    private readonly ProveedorServices _proveedorServices;
    public ProductoController(
        ILogger<ProductoController> logger,
        ProductoServices productoServices,
        ProveedorServices proveedorServices)
    {
        _logger = logger;
        _productoServices = productoServices;
        _proveedorServices = proveedorServices;
    }
    //MOSTRAR
    [HttpGet("Listar")]
    public async Task<IActionResult> GetProducto()
    {
        try
        {
            var producto = await _productoServices.GetAsync();
            return Ok(producto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los productos");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }



    //INSERTAR
    [HttpPost("Crear")]
    public async Task<IActionResult> CreateDriver([FromBody] Producto producto)
    {
        try
        {
            if (producto == null)
                return BadRequest("El producto no puede ser null");
            if (producto.Nombre == string.Empty)
                return BadRequest("El nombre del producto no puede estar vacío");

            if (!string.IsNullOrEmpty(producto.ProveedorId))
            {
                var proveedor = await _proveedorServices.GetProveedorId(producto.ProveedorId);
                if (proveedor == null)
                    return BadRequest("El proveedor no existe");
            }


            await _productoServices.InsertarProducto(producto);

            // Obtiene el nombre del proveedor después de insertar el producto
            producto = await _productoServices.ProductoPorId(producto.Id);

            return Created("Created", producto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el producto");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
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
        try
        {
            if (producto == null)
                return BadRequest("El producto no puede ser null");
            if (string.IsNullOrEmpty(producto.Nombre))
                return BadRequest("El nombre del producto no puede estar vacío");
            if (producto.Codigo == 0)
                return BadRequest("El código del producto no puede ser cero");

            producto.Id = Id;
            await _productoServices.EditarProducto(producto);

            // Obtiene el nombre del proveedor después de actualizar el producto
            producto = await _productoServices.ProductoPorId(producto.Id);
            if (producto == null)
                return NotFound($"No se encontró ningún producto con el ID: {producto.Id}");

            return Created("Created", producto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al editar el producto");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }
    //BUSQUEDA POR CODIGO 
    [HttpPost("BuscarPorCodigo")]
    public async Task<IActionResult> BuscarPorCodigo([FromBody] long codigo)
    {
        try
        {
            var producto = await _productoServices.ProductoPorCodigo(codigo);
            return Ok(producto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al obtener el producto con el código: {codigo}");
            return StatusCode(500, "Error interno del servidor o código incorrecto. Por favor, inténtalo de nuevo.");
        }
    }




    //BUSCAR POR ID
    [HttpGet("Buscar/{Id}")]
    public async Task<IActionResult> ProductoPorId(string Id)
    {
        try
        {
            var producto = await _productoServices.ProductoPorId(Id);
          //Si no jala descomentarar aqui
            //if (producto == null)
            //{
            //    return NotFound($"No se encontró ningún producto con el ID: {Id}");
            //}
            return Ok(producto);
    }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al obtener el producto con el ID: {Id}");
            return StatusCode(500, "Error interno del servidor o ID incorrecto. Por favor, inténtalo de nuevo.");
}
    }

}

