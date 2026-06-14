using GestionGimnasio.Bd.Datos.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Bd.Datos
{
    public class AppDbContext: DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}