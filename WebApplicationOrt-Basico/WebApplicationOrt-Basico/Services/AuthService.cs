using System.Linq;
using System.Threading.Tasks;
using WebApplicationOrt_Basico.Models;
using WebApplicationOrt_Basico.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

public class AuthService
{
    private readonly AppDatabaseContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(AppDatabaseContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CustomUser> AuthenticateAsync(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        return user;
    }

    public async Task<bool> RegisterUserAsync(CustomUser user)
    {
        // Verificar si el usuario ya existe
        var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null)
        {
            return false; // El usuario ya existe
        }

        // Agregar el nuevo usuario a la base de datos sin hashear la contraseña
        _context.Users.Add(user);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public CustomUser GetAuthenticatedUser()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            return _context.Users.Find(int.Parse(userId));
        }
        return null;
    }

    public async Task LogoutAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
