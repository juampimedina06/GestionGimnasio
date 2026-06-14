using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Bd.Datos.Entity
{
    public enum tipoRol
    {
        Administrador,
        Usuario
    }
    public class Rol
    {
        public int Id { get; set; }
        public tipoRol Tipo { get; set; }


    }
}
