using Microsoft.AspNetCore.Mvc;
using WebApplicationOrt_Basico.Models;
using WebApplicationOrt_Basico.Services;
using System.Threading.Tasks;
using System.Linq;

namespace WebApplicationOrt_Basico.Controllers
{
    public class TareaController : Controller
    {
        private readonly TareaService _tareaService;
        private readonly AuthService _authService;

        public TareaController(TareaService tareaService, AuthService authService)
        {
            _tareaService = tareaService;
            _authService = authService;
        }

        public IActionResult Index()
        {
            var user = _authService.GetAuthenticatedUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tareas = _tareaService.ObtenerTareas().Where(t => t.UserId == user.IdUsuario);
            return View(tareas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Tarea tarea)
        {
            var user = _authService.GetAuthenticatedUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            tarea.UserId = user.IdUsuario;
            var result = await _tareaService.CrearTareaAsync(tarea);
            if (!result)
            {
                ModelState.AddModelError("", "Usted no puede seguir creando tareas pendientes ya que tiene 5 anteriores.");
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