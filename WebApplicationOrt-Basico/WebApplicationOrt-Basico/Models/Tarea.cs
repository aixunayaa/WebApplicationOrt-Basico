using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationOrt_Basico.Models
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTarea { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [EnumDataType(typeof(Estado))]
        public Estado Estado { get; set; }

        public bool EstaAtrasado { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        [ForeignKey("CustomUser")]
        public int UserId { get; set; }
        public CustomUser CustomUser { get; set; }
    }
}