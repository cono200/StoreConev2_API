using StoreCone.Api.Models;
using StoreCone.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Drivers.Api.Models;


namespace StoreCone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly UsuarioServices _usuarioServices;
    public UsuarioController(
        ILogger<UsuarioController> logger,
        UsuarioServices usuarioServices)
    {
        _logger = logger;
        _usuarioServices = usuarioServices;
    }
    //MOSTRAR
    [HttpGet("Listar")]
    public async Task<IActionResult> GetUsuario()
    {
        try
        {
            var usuario = await _usuarioServices.GetAsync();
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los usuarios");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }

    //INSERTAR
    [HttpPost("Crear")]
    public async Task<IActionResult> CreateDriver([FromBody] UsuarioModel usuario)
    {
        try
        {
            if (usuario == null)
                return BadRequest("El usuario no puede ser null");
            if (usuario.NombreUsuario == string.Empty)
                return BadRequest("El nombre del usuario no puede estar vacío");

            await _usuarioServices.InsertUsuario(usuario);
            return Created("Created", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el usuario");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }

    //ELIMINAR
    [HttpDelete("Borrar/{Id}")]
    public async Task<IActionResult> DeleteUsuario(string Id)
    {
        try
        {
            await _usuarioServices.DeleteUsuario(Id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al borrar el usuario");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }

    //EDITAR
    [HttpPut("Editar")]
    public async Task<IActionResult> UpdateUser([FromBody] UsuarioModel usuario, string Id)
    {
        try
        {
            if (usuario == null)
                return BadRequest("El usuario no puede ser null");
            if (usuario.NombreUsuario == string.Empty)
                return BadRequest("El nombre del usuario no puede estar vacío");

            usuario.Id = Id;
            await _usuarioServices.UpdateUsuario(usuario);
            return Created("Created", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el usuario");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }

    //BUSCAR POR ID
    [HttpGet("Buscar/{Id}")]
    public async Task<IActionResult> UsuarioPorId(string Id)
    {
        try
        {
            var usuario = await _usuarioServices.GetUsuarioId(Id);
            if (usuario == null)
                return NotFound($"No se encontró ningún usuario con el ID: {Id}");

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el usuario por ID");
            return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
        }
    }
}