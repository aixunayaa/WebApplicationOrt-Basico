using Microsoft.EntityFrameworkCore;
using WebApplicationOrt_Basico.Models;

namespace WebApplicationOrt_Basico.Context
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
        {
        }

        public DbSet<CustomUser> Users { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configuraciones adicionales si es necesario
        }
    }
}