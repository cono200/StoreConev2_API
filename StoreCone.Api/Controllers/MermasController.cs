using StoreCone.Api.Models;
using StoreCone.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace StoreCone.Api.Controllers;
[ApiController]
[Route("api/[controller]")]


public class MermasController : ControllerBase
{

    private readonly ILogger<MermasController> _logger;
    private readonly MermaServices _mermasServices;


    public MermasController(
      ILogger<MermasController> logger,
      MermaServices mermasServices)
    {
        _logger = logger;
        _mermasServices = mermasServices;
    }


    //MOSTRAR
    [HttpGet("Listar")]
    public async Task<IActionResult> GetMermas()
    {
        try
        {
            var proveedor = await _mermasServices.GetAsync();
            return Ok(proveedor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los proveedores");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }

    //BUSCAR POR ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMermabyId(string id)
    {
        try
        {
            var merma = await _mermasServices.GetMermabyId(id);
            if (merma == null)
                return NotFound($"No se encontró ninguna merma con el ID: {id}");

            return Ok(merma);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la merma por ID");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }

    //INSERTAR
    [HttpPost("Insertar")]
    public async Task<IActionResult> InsertMerma([FromBody] MermasModel mermas)
    {
        try
        {
            if (mermas == null)
                return BadRequest("La merma no puede ser null");
            if (mermas.Codigo == 0)
                return BadRequest("El código de la merma no puede estar vacío");

            await _mermasServices.InsertMerma(mermas);
            return Created("Created", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al insertar la merma");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }

    //ACTUALIZAR
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateMerma([FromBody] MermasModel mermas, string id)
    {
        try
        {
            if (mermas == null)
                return BadRequest("La merma no puede ser null");
            if (mermas.Codigo == 0)
                return BadRequest("El código de la merma no puede estar vacío");

            mermas.Id = id;
            await _mermasServices.UpdateMerma(mermas);
            return Created("Created", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar la merma");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }

    //ELIMINAR
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteProveedor(string id)
    {
        try
        {
            await _mermasServices.DeleteMerma(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar la merma");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }
}


