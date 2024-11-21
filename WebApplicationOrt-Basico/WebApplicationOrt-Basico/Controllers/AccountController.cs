using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplicationOrt_Basico.Models;
using WebApplicationOrt_Basico.Services;
using WebApplicationOrt_Basico.ViewModels;

namespace WebApplicationOrt_Basico.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _authService.AuthenticateAsync(email, password);
            if (user != null)
            {
                return RedirectToAction("Index", "Tarea");
            }

            ViewBag.ErrorMessage = "Email o contraseña incorrectos.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new CustomUser
                {
                    Email = model.Email,
                    Password = model.Password,  
                    Apodo = model.Apodo,
                    FechaInscripto = model.FechaInscripto,
                    Genero = model.Genero
                };

                var result = await _authService.RegisterUserAsync(user);
                if (result)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError(string.Empty, "Error al registrar el usuario.");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }
    }
}