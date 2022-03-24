using System;
using System.Collections.Generic;

namespace SeguridadCiudadana.Domain.Dtos
{
    public partial class DireccionCreateRequest
    {
        public int Iddireccionsegura { get; set; }

        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public int Idpeligro { get; set; }
        

    }


}