using StoreCone.Api.Models;
using StoreCone.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace StoreCone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]


public class ProveedorController : ControllerBase
{

    private readonly ILogger<ProveedorController> _logger;
    private readonly ProveedorServices _proveedorServices;


    public ProveedorController(
        ILogger<ProveedorController> logger,
        ProveedorServices proveedorServices)
    {
        _logger = logger;
        _proveedorServices = proveedorServices;
    }


    [HttpGet]
    public async Task<IActionResult> GetProveedor()
    {
        var proveedor = await _proveedorServices.GetAsync();
        return Ok(proveedor);
    }


    [HttpGet("{id}")]

    public async Task<IActionResult> GetProveedorById(string id)
    {
        return Ok(await _proveedorServices.GetProveedorId(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProveedor([FromBody] Proveedor proveedor)
    {
        if(proveedor == null)
        {
            return BadRequest();
        }
        if(proveedor.Nombre == string.Empty)
        {
            ModelState.AddModelError("Nombre", "El proveedor no debe de estar vacio");
        }
        
        await _proveedorServices.InsertProveedor(proveedor);
        return Created("Created", true);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateProveedor([FromBody] Proveedor proveedor, string id)
    {
        if(proveedor == null)
        {
            return BadRequest();
        }
        if (proveedor.Nombre == string.Empty)
        {
            ModelState.AddModelError("Nombre", "El proveedor no deberia estar vacio");
        }

        proveedor.Id = id;

        await _proveedorServices.UpdateProveedor(proveedor);
        return Created("Created", true);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteProveedor(string id)
    {
        await _proveedorServices.DeleteProveedor(id);

        return NoContent();
    }


}