using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Bd.Datos.Entity
{
    public enum MetodoPago
    {
        Efectivo,
        Transferencia
    }

    public enum EstadoPago
    {
        AlDia,
        Vencido,
        EnTolerancia
    }

    public class Pago
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int IdPlan { get; set; }
        public Plan Plan { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Monto { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public EstadoPago Estado { get; set; }
    }
}
