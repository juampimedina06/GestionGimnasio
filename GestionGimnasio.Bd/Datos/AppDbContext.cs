using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Bd.Datos
{
    public class AppDbContext: DbContext
    {
        

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
