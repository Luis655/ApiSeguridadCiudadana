using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadCiudadana.Domain.Dtos
{
    public partial class UsuariosCreateRequest
    {
        public int Idusuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechNac { get; set; }
        public int? Idgenero { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string Cruzamientos { get; set; }
        
         
    }
}