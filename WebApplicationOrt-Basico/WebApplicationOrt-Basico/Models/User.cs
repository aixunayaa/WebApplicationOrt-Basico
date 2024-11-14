using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationOrt_Basico.Models
{
    public class User : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        public string Apodo { get; set; } = string.Empty;

        [Display(Name = "Fecha inscripción")]
        public DateTime FechaInscripto { get; set; }

        [EnumDataType(typeof(Genero))]
        public Genero Genero { get; set; }
    }
}
