using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplicationOrt_Basico.Models;
using WebApplicationOrt_Basico.Services;

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

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }
    }
}
