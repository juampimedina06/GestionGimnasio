using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Bd.Datos.Entity
{
    public enum TipoPlan
    {
        Mensual,
        Semanal
    }

    public class Plan
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoPlan Tipo { get; set; }
        public int DiasValidez { get; set; }
        public decimal Precio { get; set; }
        public int ToleranciaDias { get; set; }
    }
}
