using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationOrt_Basico.Models;
using WebApplicationOrt_Basico.Context;

namespace WebApplicationOrt_Basico.Services
{
    public class TareaService
    {
        private readonly AppDatabaseContext _context;

        public TareaService(AppDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Tarea> ObtenerTareas()
        {
            return _context.Tareas.ToList();
        }

        public Tarea ObtenerTareaPorId(int id)
        {
            return _context.Tareas.Find(id);
        }

        public async Task<bool> CrearTareaAsync(Tarea tarea)
        {
            var tareasPendientes = _context.Tareas
                .Where(t => t.UserId == tarea.UserId && t.Estado == Estado.PENDIENTE)
                .Count();

            if (tareasPendientes >= 5)
            {
                return false;
            }

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ActualizarTareaAsync(Tarea tarea)
        {
            _context.Tareas.Update(tarea);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarTareaAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();
            }
        }
    }
}