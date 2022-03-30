using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadCiudadana.Domain.Dtos
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
         public int Tipo { get; set; }

    }
}