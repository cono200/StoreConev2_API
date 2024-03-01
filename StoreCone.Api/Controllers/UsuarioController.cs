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
        var usuario = await _usuarioServices.GetAsync();
        return Ok(usuario);
    }


    //INSERTAR
    [HttpPost("Crear")]
    public async Task<IActionResult> CreateDriver([FromBody] UsuarioModel usuario)
    {
        if (usuario == null)
            return BadRequest();
        if (usuario.NombreUsuario == string.Empty)
            ModelState.AddModelError("Nombre", "El producto no debe estar vacio");

        await _usuarioServices.InsertUsuario(usuario);

        return Created("Created", true);
    }

    //ELIMINAR
    [HttpDelete("Borrar/{Id}")]
    public async Task<IActionResult> DeleteUsuario(string Id)
    {
        await _usuarioServices.DeleteUsuario(Id);
        return NoContent();
    }

    //EDITAR
    [HttpPut("Editar")]
    public async Task<IActionResult> UpdateUser([FromBody] UsuarioModel usuario, string Id)
    {
        if (usuario == null)
            return BadRequest();
        if (usuario.NombreUsuario == string.Empty)
            ModelState.AddModelError("Nombre", "El usuario no debe estar vacio");
        usuario.Id = Id;

        await _usuarioServices.UpdateUsuario(usuario);
        return Created("Created", true);
    }

    //BUSCAR POR ID
    [HttpGet("Buscar/{Id}")]
    public async Task<IActionResult> UsuarioPorId(string Id)
    {
        return Ok(await _usuarioServices.GetUsuarioId(Id));
    }
}

