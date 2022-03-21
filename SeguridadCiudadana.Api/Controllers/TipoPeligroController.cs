using System.Xml;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using SeguridadCiudadana.Domain.Entities;
using SC.Infrastructure.Repositories;
using SeguridadCiudadana.Domain.Dtos;
using System.Threading.Tasks;


namespace SeguridadCiudadana.Api.Controllers{}

[ApiController]
[Route("[controller]")]
public class TipoPeligroController : ControllerBase
{

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var _context = new RepoSql();
        var respuesta = await _context.GetAllTiposdePeligro();
        return Ok(respuesta);
    }
}