using System;
using System.Collections.Generic;

namespace SeguridadCiudadana.Domain.Entities
{
    public partial class DireccionessegurasResponse
    {
      public int  Iddireccionsegura { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
         public string Tipopeligro { get; set; }
        public string Descripcion { get; set; }

    }
}