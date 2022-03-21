using System;
using System.Collections.Generic;

namespace SeguridadCiudadana.Domain.Dtos
{
    public partial class DireccionCreateRequest
    {
        public int Iddireccionsegura { get; set; }

        public int? Latitud { get; set; }
        public int? Longitud { get; set; }
        public int Idpeligro { get; set; }
        

    }


}