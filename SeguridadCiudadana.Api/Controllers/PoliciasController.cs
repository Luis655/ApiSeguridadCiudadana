using System.Xml;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using SeguridadCiudadana.Domain.Entities;
using SC.Infrastructure.Repositories;
using SeguridadCiudadana.Domain.Dtos;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace SeguridadCiudadana.Api.Controllers {

[ApiController]
[Route("[controller]")]
public class PoliciasController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContext;
    public PoliciasController(IHttpContextAccessor httpContext)
    {           
         this._httpContext = httpContext;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var _context = new RepoSql();
        var respuesta = await _context.GetAll();
        var respuesta2 = respuesta.Select(x => CreateDTOFromObjects2(x));
        return Ok(respuesta2);
    }


     [HttpGet]
    [Route("GetAllCargos")]
    public async Task<IActionResult> GetAllcargos()
    {
        var _context = new RepoSql();
        var respuesta = await _context.GetAllcargos();
       
        return Ok(respuesta);
    }


    [HttpPost]
    [Route("GetUser")]
    public async Task<IActionResult> GetUser([FromBody]Tipousuario tipousuario)
    {
        var _context = new RepoSql();
        
        var respuesta = await _context.GetUser(tipousuario);

        if (respuesta == null)
        {
            return StatusCode(500);
        }
        else
        {
             return Ok(respuesta);
        }
       
    }


    [HttpGet]
    [Route("GetById/{id:int}")]
      public async Task<IActionResult> GetById(int id)
       {
            var _context = new RepoSql();
             
            var policia =await _context.GetById2(id);
            

            var respuesta2 = CreateDTOFromObjects2(policia);
            
            return Ok(respuesta2);
       } 

     [HttpPost]
     [Route("Create")]
        public async Task<IActionResult> Create([FromBody]PoliciaCreateRequest policia)
        {
            

            var entity = CreateObjctFromDTO(policia);
             var _context = new RepoSql();
             var id = await _context.Create(entity);

            var urlresult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/person/{id}";
            return Created(urlresult, id);
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PoliciaCreateRequest policia)
        {
            var _context = new RepoSql();
            if(id <= 0)
                return NotFound("El registro no fué encontrado, veifica tu información...");
            policia.Idpolicias = id;
            var entity = CreateObjctFromDTO(policia);
            var update = await _context.Update(id, entity);
            if(!update)
                return Conflict("Ocurrió un fallo al intentar realizar la modificación...");
            return NoContent();            
        }


        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _repo = new RepoSql();
            var _context =await _repo.Delete(id);
           
            if(_context==false)
                return NotFound("no se encontraron valores");
            
            return NoContent();
        } 


    private PoliciaResponse CreateDTOFromObjects(Policia policia)
    {
         var dtos = new PoliciaResponse{
         Idpolicias=policia.Idpolicias,
         NombreCompleto = policia.IdpersonaNavigation == null ? string.Empty : $"{policia.IdpersonaNavigation.Nombre} {policia.IdpersonaNavigation.Apellidos}",
         FechNac = policia.IdpersonaNavigation == null ? Convert.ToDateTime("00-00-0000") : policia.IdpersonaNavigation.FechNac,
         Numeroplaca = policia.Numeroplaca,
         Tipoderango = policia.IdrangoNavigation == null ? string.Empty : policia.IdrangoNavigation.Tipoderango,
         Nombreestacion = policia.IdestacionNavigation == null ? string.Empty : policia.IdestacionNavigation.Nombreestacion,
         Direccion = policia.IdestacionNavigation == null ? string.Empty : $"{policia.IdestacionNavigation.IddireccionNavigation.Estado} {policia.IdestacionNavigation.IddireccionNavigation.Municipio} {policia.IdestacionNavigation.IddireccionNavigation.Colonia} {policia.IdestacionNavigation.IddireccionNavigation.Calle} {policia.IdestacionNavigation.IddireccionNavigation.Cruzamientos}",
         Nombre = policia.IdpersonaNavigation == null ? string.Empty : policia.IdpersonaNavigation.Nombre,
         Apellidos = policia.IdpersonaNavigation == null ? string.Empty : policia.IdpersonaNavigation.Apellidos,
         Idrango =policia.IdrangoNavigation== null ? null : policia.IdrangoNavigation.Idrango,
         Idestacion = policia.IdestacionNavigation == null ? null : policia.IdestacionNavigation.Idestacion,
         Correo = policia.IdtipousuarioNavigation == null ? string.Empty : policia.IdtipousuarioNavigation.Correo,
         Contraseña = policia.IdtipousuarioNavigation == null ? string.Empty : policia.IdtipousuarioNavigation.Contraseña
            };

         return dtos;
    }




     private PoliciaResponse CreateDTOFromObjects2(Policia policia)
    {
         var dtos = new PoliciaResponse{ 
         Idpolicias=policia.Idpolicias,
         NombreCompleto = policia.IdpersonaNavigation == null ? string.Empty : $"{policia.IdpersonaNavigation.Nombre} {policia.IdpersonaNavigation.Apellidos}",
         FechNac = policia.IdpersonaNavigation == null ? Convert.ToDateTime("00-00-0000") : policia.IdpersonaNavigation.FechNac,
         Numeroplaca = policia.Numeroplaca,
         Tipoderango = policia.IdrangoNavigation == null ? string.Empty : policia.IdrangoNavigation.Tipoderango,
         Nombreestacion = policia.IdestacionNavigation == null ? string.Empty : policia.IdestacionNavigation.Nombreestacion,
         
         Direccion = policia.IdestacionNavigation == null ? string.Empty : $"{policia.IdestacionNavigation.IddireccionNavigation.Estado} {policia.IdestacionNavigation.IddireccionNavigation.Municipio} {policia.IdestacionNavigation.IddireccionNavigation.Colonia} {policia.IdestacionNavigation.IddireccionNavigation.Calle} {policia.IdestacionNavigation.IddireccionNavigation.Cruzamientos}",
         genero = policia.IdgeneroNavigation == null ? string.Empty : policia.IdgeneroNavigation.Tipigenero,
         Nombre = policia.IdpersonaNavigation == null ? string.Empty : policia.IdpersonaNavigation.Nombre,
         Apellidos = policia.IdpersonaNavigation == null ? string.Empty : policia.IdpersonaNavigation.Apellidos,
         Idrango =policia.IdrangoNavigation== null ? null : policia.IdrangoNavigation.Idrango,
    
         Idestacion = policia.IdestacionNavigation == null ? null : policia.IdestacionNavigation.Idestacion,
         Correo = policia.IdtipousuarioNavigation == null ? string.Empty : policia.IdtipousuarioNavigation.Correo,
         Contraseña = policia.IdtipousuarioNavigation == null ? string.Empty : policia.IdtipousuarioNavigation.Contraseña
            };

         return dtos;
    }




     

    private Policia CreateObjctFromDTO(PoliciaCreateRequest dto)
        {
            var policia = new Policia{
                Idpolicias = dto.Idpolicias,
                IdpersonaNavigation = new Persona{
                    Nombre = dto.Nombre,
                    Apellidos = dto.Apellidos,
                    FechNac = dto.FechNac},
                Numeroplaca = dto.Numeroplaca,
                Idrango = dto.Idrango,
                Idestacion = dto.Idestacion,
                IdtipousuarioNavigation = new Tipousuario{
                    Idcargo = 2,
                    Correo = dto.Correo,
                    Contraseña = dto.Contraseña},
                Idgenero = dto.Idgenero
            };
            

            return policia;
        }



  
    

}
 }

