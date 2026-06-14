using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Bd.Datos.Entity
{
    public enum EstadoCliente
    {
        Activo,
        Inactivo,
        Baja
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
        public EstadoCliente Estado { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public Usuario UsuarioRegistro { get; set; }
    }
}
