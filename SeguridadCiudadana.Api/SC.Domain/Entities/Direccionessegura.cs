using System;
using System.Collections.Generic;

namespace SeguridadCiudadana.Domain.Entities
{
    public partial class Direccionessegura
    {
        public int Iddireccionsegura { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public int Idpeligro { get; set; }
        public bool? Verificacion { get; set; }
        public int? Calificacion { get; set; }

        public virtual Categoriapeligro IdpeligroNavigation { get; set; }
    }
}
