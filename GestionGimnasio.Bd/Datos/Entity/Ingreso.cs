using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Bd.Datos.Entity
{
    public class Ingreso
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Permitido { get; set; }
        public string Observacion { get; set; }
    }
}
