using Microsoft.AspNetCore.Mvc;
using StoreCone.Api.Services;
using StoreCone.Api.Models;


namespace StoreCone.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
    public class PistolaController : ControllerBase
    {
    private readonly ILogger<PistolaController> _logger;
    private readonly PistolaServices _pistolaServices;


    public PistolaController(
      ILogger<PistolaController> logger,
      PistolaServices pistolaServices)
    {
        _logger = logger;
        _pistolaServices = pistolaServices;
    }



    [HttpPost("EscanearProducto")]
    public async Task<IActionResult> InsertarPistola([FromBody] Pistola pistola)
    {
        try
        {
            await _pistolaServices.InsertarPistola(pistola);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpGet("ultimo")]
    public async Task<IActionResult> ObtenerUltimaPistolaRegistrada()
    {
        try
        {
            var pistola = await _pistolaServices.ObtenerUltimaPistolaRegistrada();
            return Ok(pistola);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



}

