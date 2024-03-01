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


    [HttpGet]
    public async Task<IActionResult> GetProveedor()
    {
        var proveedor = await _mermasServices.GetAsync();
        return Ok(proveedor);
    }


    [HttpGet("{id}")]

    public async Task<IActionResult> GetProveedorById(string id)
    {
        return Ok(await _mermasServices.GetProveedorId(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProveedor([FromBody] MermasModel mermas)
    {
        if (mermas == null)
        {
            return BadRequest();
        }
        if (mermas.Codigo == string.Empty)
        {
            ModelState.AddModelError("Codigo", "El codigo no debe de estar vacio");
        }

        await _mermasServices.InsertMerma(mermas);
        return Created("Created", true);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateProveedor([FromBody] MermasModel mermas, string id)
    {
        if (mermas == null)
        {
            return BadRequest();
        }
        if (mermas.Codigo == string.Empty)
        {
            ModelState.AddModelError("Codigo", "El Codigo no deberia estar vacio");
        }

        mermas.Id = id;

        await _mermasServices.UpdateMerma(mermas);
        return Created("Created", true);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteProveedor(string id)
    {
        await _mermasServices.DeleteMerma(id);

        return NoContent();
    }
}

