using StoreCone.Api.Models;
using StoreCone.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace StoreCone.Api.Controllers;
[ApiController]
[Route("api/[controller]")]


public class HistorialController : ControllerBase
{

    private readonly ILogger<HistorialController> _logger;
    private readonly HistorialServices _historialServices;


    public HistorialController(
      ILogger<HistorialController> logger,
      HistorialServices mermasServices)
    {
        _logger = logger;
        _historialServices = mermasServices;
    }


    //MOSTRAR
    [HttpGet("Listar")]
    public async Task<IActionResult> GetMermas()
    {
        try
        {
            var historial = await _historialServices.GetAsync();
            return Ok(historial);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el historial");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }

    //BUSCAR POR ID
   


    //ELIMINAR
    
}


