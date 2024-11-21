using System.ComponentModel.DataAnnotations;
using WebApplicationOrt_Basico.Models;

namespace WebApplicationOrt_Basico.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña ingresada no existe o es incorrecta. Por favor, verifique los datos ingresados y vuelva a intentarlo.")]
        public string ConfirmPassword { get; set; }

        public string Apodo { get; set; }

        [Display(Name = "Fecha inscripción")]
        public DateTime FechaInscripto { get; set; }

        [EnumDataType(typeof(Genero))]
        public Genero? Genero { get; set; }
    }
}