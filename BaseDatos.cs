using Microsoft.EntityFrameworkCore;

namespace Entregable3
{
    public class BaseDatos : DbContext
    {
        public BaseDatos(DbContextOptions<BaseDatos> options)
            : base(options) { }

        public DbSet<Contenido> Contenido => Set<Contenido>();
    }
}
