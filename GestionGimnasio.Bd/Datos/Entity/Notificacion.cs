using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Bd.Datos.Entity
{
    public enum TipoNotificacion
    {
        Email,
        WhatsApp
    }

    public enum EstadoNotificacion
    {
        Pendiente,
        Enviado,
        Error
    }

    public class Notificacion
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public TipoNotificacion Tipo { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public EstadoNotificacion EstadoEnvio { get; set; }
    }
}
