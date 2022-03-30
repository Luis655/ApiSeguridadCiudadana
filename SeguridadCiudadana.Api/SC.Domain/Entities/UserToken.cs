using System;
using System.Collections.Generic;
using System.Text;

namespace SeguridadCiudadana.Domain.Entities
{
    public class UserToken
    {
        public string Token { get; set; }
        public string Rol { get; set; }
        public DateTime Expiration { get; set; }
    }
}