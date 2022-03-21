
using System;
using System.IO;
using System.Xml;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using SeguridadCiudadana.Domain.Entities;
using SC.Infrastructure.Repositories;
using SeguridadCiudadana.Domain.Dtos;
using System.Threading.Tasks;


namespace SeguridadCiudadana.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LugaresController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContext;

        public LugaresController(IHttpContextAccessor httpContext)
        {
            this._httpContext = httpContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var _context = new RepoSql();
            var respuesta = await _context.GetAllRutas();
            var respuesta2 = respuesta.Select(x => CreateDTOFromObjects(x));
            return Ok(respuesta2);

        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var _context = new RepoSql();

            var Direccionessegura = await _context.GetByIdDireccionesseguras2(id);


            var respuesta2 = CreateDTOFromObjects(Direccionessegura);

            return Ok(respuesta2);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] DireccionCreateRequest direccion)
        {


            var entity = CreateObjctFromDTO(direccion);
            var _context = new RepoSql();
            var id = await _context.CreateDireccionsegura(entity);

            var urlresult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/person/{id}";
            return Created(urlresult, id);
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] DireccionCreateRequest direccion)
        {
            var _context = new RepoSql();
            if (id <= 0)
                return NotFound("El registro no fue encontrado, veifica tu informacion...");
            direccion.Iddireccionsegura = id;
            var entity = CreateObjctFromDTO(direccion);
            var update = await _context.UpdateDireccionessegura(id, entity);
            if (!update)
                return Conflict("Ocurrio un fallo al intentar realizar la modificacion...");
            return NoContent();
        }


        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _repo = new RepoSql();
            var _context = await _repo.DeleteDireccionesseguras(id);

            if (_context == false)
                return NotFound("no se encontraron valores");

            return NoContent();
        }



        private DireccionessegurasResponse CreateDTOFromObjects(Direccionessegura direccion)
        {
            var dtos = new DireccionessegurasResponse
            {
                Iddireccionsegura = direccion.Iddireccionsegura,
                Latitud = direccion.Latitud == null ? null : direccion.Latitud,
                Longitud = direccion.Latitud == null ? null : direccion.Latitud,
                Tipopeligro = direccion.IdpeligroNavigation == null ? null : direccion.IdpeligroNavigation.Tipopeligro,
                Descripcion = direccion.IdpeligroNavigation == null ? null : direccion.IdpeligroNavigation.Descripcion
                
            };
            return dtos;
        }

        private DireccionessegurasResponse CreateDTOFromObjects2(Direccionessegura direccion)
        {
            var dtos = new DireccionessegurasResponse
            {
                Latitud = direccion.Latitud == null ? null : direccion.Latitud,
                Longitud = direccion.Latitud == null ? null : direccion.Latitud,
               


            };

            return dtos;
        }

        private Direccionessegura CreateObjctFromDTO(DireccionCreateRequest dto)
        {
            var direccion = new Direccionessegura
            {
                Iddireccionsegura = dto.Iddireccionsegura,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud,
                Idpeligro = dto.Idpeligro 

            };
            
            

            return direccion;
        }

    }
}