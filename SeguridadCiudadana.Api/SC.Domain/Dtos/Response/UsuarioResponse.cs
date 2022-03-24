using System;
using SeguridadCiudadana.Domain.Entities;
using System.Collections.Generic;

namespace SeguridadCiudadana.Domain.Dtos
{
    public partial class UsuarioResponse
    {
        public int Idusuario { get; set; }
        public string? NombreCompleto { get; set; }
        public DateTime? FechNac { get; set; }
        public string? genero { get; set; }
        public string? Direccion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string Cruzamientos { get; set; }

        //public int? idgenero { get; set; }

        

    }
}