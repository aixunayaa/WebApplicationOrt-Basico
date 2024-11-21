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


        [HttpPost]
        public IActionResult Filtrar(string estado)
        {
            var user = _authService.GetAuthenticatedUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tareas = _tareaService.ObtenerTareas().Where(t => t.UserId == user.IdUsuario);

            if (!string.IsNullOrEmpty(estado))
            {
                Enum.TryParse(estado, out Estado estadoEnum);
                tareas = tareas.Where(t => t.Estado == estadoEnum);
            }

            return View("Index", tareas);
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
        [HttpPost]
        public async Task<IActionResult> Completar(int id, bool completar)
        {
            var tarea = await _tareaService.ObtenerTareaPorId(id);
            if (tarea == null)
            {
                return NotFound();
            }

            tarea.Estado = completar ? Estado.FINALIZADO : Estado.PENDIENTE;
            var resultado = await _tareaService.ActualizarTareaAsync(tarea);

            if (!resultado)
            {
                return StatusCode(500, "Error al actualizar la tarea.");
            }

            return Ok();
        }

           
        

        public async Task<IActionResult> Editar(int id)
        {
            var tarea = await _tareaService.ObtenerTareaPorId(id);
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
                var resultado = await _tareaService.ActualizarTareaAsync(tarea);
                if (!resultado)
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }
            return View(tarea);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var tarea = await _tareaService.ObtenerTareaPorId(id);
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