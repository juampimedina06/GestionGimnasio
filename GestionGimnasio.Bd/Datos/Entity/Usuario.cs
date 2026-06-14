using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Bd.Datos.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }
        public int IdRol { get; set; }
        public Rol Rol { get; set; }
    }
}
