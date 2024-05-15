using System.ComponentModel.DataAnnotations;
namespace RegistroTecnicos.Models
{
    public class TiposTecnicos
    {

        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "El TipoId debe ser mayor o igual que 1.")]
        public int TipoId { get; set; }

        [Required(ErrorMessage ="El campo descripción es obligatorio")]
        public string? Descripcion { get; set; }
         
        public string Tipo { get; set; }
    }
}
