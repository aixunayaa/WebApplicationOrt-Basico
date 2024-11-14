using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrt_Basico.Context;
using WebApplicationOrt_Basico.Models;
using WebApplicationOrt_Basico.Services;

namespace WebApplicationOrt_Basico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar el contexto de la base de datos
            builder.Services.AddDbContext<AppDatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configurar Identity
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDatabaseContext>()
                .AddDefaultTokenProviders();

            // Registrar el servicio de tareas
            builder.Services.AddScoped<TareaService>();

            // Agregar servicios al contenedor
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configurar el pipeline de solicitudes HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}