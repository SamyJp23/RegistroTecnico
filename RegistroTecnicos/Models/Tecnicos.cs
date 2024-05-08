using System.ComponentModel.DataAnnotations;
namespace RegistroTecnicos.Models
{
    public class Tecnicos
    {
        [Key]
        public int IdTecnico { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string? Nombre { get; set; }

        public double SueldoHora { get; set; }
    }
}
