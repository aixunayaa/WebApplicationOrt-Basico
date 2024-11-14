using Microsoft.AspNetCore.Mvc;
using WebApplicationOrt_Basico.Models;
using WebApplicationOrt_Basico.Services;
using System.Threading.Tasks;

namespace WebApplicationOrt_Basico.Controllers
{
    public class TareaController : Controller
    {
        private readonly TareaService _tareaService;

        public TareaController(TareaService tareaService)
        {
            _tareaService = tareaService;
        }

        public IActionResult Index()
        {
            var tareas = _tareaService.ObtenerTareas();
            return View(tareas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Tarea tarea)
        {
            var result = await _tareaService.CrearTareaAsync(tarea);
            if (!result)
            {
                ModelState.AddModelError("", "No puedes tener más de 5 tareas pendientes.");
                return View(tarea);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var tarea = _tareaService.ObtenerTareaPorId(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                await _tareaService.ActualizarTareaAsync(tarea);
                return RedirectToAction("Index");
            }
            return View(tarea);
        }

        public IActionResult Eliminar(int id)
        {
            var tarea = _tareaService.ObtenerTareaPorId(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            await _tareaService.EliminarTareaAsync(id);
            return RedirectToAction("Index");
        }
    }
}
