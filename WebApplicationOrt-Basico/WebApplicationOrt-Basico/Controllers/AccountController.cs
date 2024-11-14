using Microsoft.AspNetCore.Mvc;
using WebApplicationOrt_Basico.Models;
using WebApplicationOrt_Basico.Services;
using System.Threading.Tasks;

namespace WebApplicationOrt_Basico.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.AuthenticateAsync(model.Email, model.Password);
                if (user != null)
                {
                    // Aquí puedes agregar la lógica para establecer la sesión del usuario
                    // Por 