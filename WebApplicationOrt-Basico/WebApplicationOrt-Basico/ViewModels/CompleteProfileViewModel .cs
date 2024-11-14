using System;
using System.ComponentModel.DataAnnotations;
using WebApplicationOrt_Basico.Models;

namespace WebApplicationOrt_Basico.ViewModels
{
    public class CompleteProfileViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Apodo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Fecha inscripción")]
        public DateTime FechaInscripto { get; set; }

        [Required]
        [EnumDataType(typeof(Genero))]
        public Genero Genero { get; set; }
    }
}