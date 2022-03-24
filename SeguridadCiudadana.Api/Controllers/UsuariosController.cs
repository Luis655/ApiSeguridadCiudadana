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
using System.Threading.Tasks;


namespace SeguridadCiudadana.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContext;
        public UsuariosController(IHttpContextAccessor httpContext)
        {
            this._httpContext = httpContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var _context = new RepoSql();
            var respuesta = await _context.GetAllUsuarios();
            var respuesta2 = respuesta.Select(x => CreateDTOFromObjects(x));
            return Ok(respuesta2);

        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var _context = new RepoSql();

            var usuario = await _context.GetByIdUsuarios2(id);


            var respuesta2 = CreateDTOFromObjects(usuario);

            return Ok(respuesta2);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UsuariosCreateRequest usuario)
        {


            var entity = CreateObjctFromDTO(usuario);
            var _context = new RepoSql();
            var id = await _context.CreateUsuario(entity);

            var urlresult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/person/{id}";
            return Created(urlresult, id);
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuariosCreateRequest usuario)
        {
            var _context = new RepoSql();
            if (id <= 0)
                return NotFound("El registro no fu� encontrado, veifica tu informaci�n...");
            usuario.Idusuario = id;
            var entity = CreateObjctFromDTO(usuario);
            var update = await _context.UpdateUsuarios(id, entity);
            if (!update)
                return Conflict("Ocurri� un fallo al intentar realizar la modificaci�n...");
            return NoContent();
        }


        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _repo = new RepoSql();
            var _context = await _repo.DeleteUsuario(id);

            if (_context == false)
                return NotFound("no se encontraron valores");

            return NoContent();
        }



        private UsuarioResponse CreateDTOFromObjects(Usuario usuario)
        {
            var dtos = new UsuarioResponse
            {
                Idusuario = usuario.Idusuario,

                NombreCompleto = usuario.IdpersonaNavigation == null ? string.Empty : $"{usuario.IdpersonaNavigation.Nombre} {usuario.IdpersonaNavigation.Apellidos}",
                FechNac = usuario.IdpersonaNavigation == null ? null : usuario.IdpersonaNavigation.FechNac,
                Direccion = usuario.IddireccionNavigation == null ? string.Empty : $"{usuario.IddireccionNavigation.Estado} {usuario.IddireccionNavigation.Municipio} {usuario.IddireccionNavigation.Colonia} {usuario.IddireccionNavigation.Calle} {usuario.IddireccionNavigation.Cruzamientos}",
                Nombre = usuario.IdpersonaNavigation == null ? string.Empty : usuario.IdpersonaNavigation.Nombre,
                Apellidos = usuario.IdpersonaNavigation == null ? string.Empty : usuario.IdpersonaNavigation.Apellidos,
                Correo = usuario.IdtipousuarioNavigation == null ? string.Empty : usuario.IdtipousuarioNavigation.Correo,
                Contraseña = usuario.IdtipousuarioNavigation == null ? string.Empty : usuario.IdtipousuarioNavigation.Contraseña,
                genero = usuario.IdgeneroNavigation == null ? string.Empty : usuario.IdgeneroNavigation.Tipigenero,
                Estado = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Estado,
                Municipio = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Municipio,
                Colonia = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Colonia,
                Calle = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Calle,
                Cruzamientos = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Cruzamientos,

            };
            return dtos;
        }

        private UsuarioResponse CreateDTOFromObjects2(Usuario usuario)
        {
            var dtos = new UsuarioResponse
            {
                Idusuario = usuario.Idusuario,

                NombreCompleto = usuario.IdpersonaNavigation == null ? string.Empty : $"{usuario.IdpersonaNavigation.Nombre} {usuario.IdpersonaNavigation.Apellidos}",
                FechNac = usuario.IdpersonaNavigation == null ? null : usuario.IdpersonaNavigation.FechNac,
                Direccion = usuario.IddireccionNavigation == null ? string.Empty : $"{usuario.IddireccionNavigation.Estado} {usuario.IddireccionNavigation.Municipio} {usuario.IddireccionNavigation.Colonia} {usuario.IddireccionNavigation.Calle} {usuario.IddireccionNavigation.Cruzamientos}",
                Nombre = usuario.IdpersonaNavigation == null ? string.Empty : usuario.IdpersonaNavigation.Nombre,
                Apellidos = usuario.IdpersonaNavigation == null ? string.Empty : usuario.IdpersonaNavigation.Apellidos,
                Correo = usuario.IdtipousuarioNavigation == null ? string.Empty : usuario.IdtipousuarioNavigation.Correo,
                Contraseña = usuario.IdtipousuarioNavigation == null ? string.Empty : usuario.IdtipousuarioNavigation.Contraseña,
                genero = usuario.IdgeneroNavigation == null ? string.Empty : usuario.IdgeneroNavigation.Tipigenero,
                Estado = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Estado,
                Municipio = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Municipio,
                Colonia = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Colonia,
                Calle = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Calle,
                Cruzamientos = usuario.IddireccionNavigation == null ? string.Empty : usuario.IddireccionNavigation.Cruzamientos,

        };

            return dtos;
        }






        private Usuario CreateObjctFromDTO(UsuariosCreateRequest dto)
        {
            var usuario = new Usuario
            {
                Idusuario = dto.Idusuario,
                IdpersonaNavigation = new Persona
                {
                    Nombre = dto.Nombre,
                    Apellidos = dto.Apellidos,
                    FechNac = dto.FechNac
                },
                IdtipousuarioNavigation = new Tipousuario{
                    Correo = dto.Correo,
                    Contraseña = dto.Contraseña,
                    Idcargo = 2
                },
                IddireccionNavigation = new Direccione{
                    Estado = dto.Estado,
                    Municipio = dto.Municipio,
                    Colonia = dto.Colonia,
                    Calle = dto.Calle,
                    Cruzamientos = dto.Cruzamientos
                },
                Idgenero = dto.Idgenero,
                

               
               /* {
                    
                    Correo = dto.Correo,
                    Contrase�a = dto.Contrase�a
                },*/
            };

            return usuario;
        }

    }
}