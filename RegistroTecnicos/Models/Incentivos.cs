using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

    public class Incentivos
    {
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor o igual que 1.")]
    public int IncentivoId { get; set; }
    [Required(ErrorMessage = "El campo descripcion es obligatorio.")]
    public string Descripcion { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "La Cantidad de servicios debe ser mayor a 0.")]
    public int CantidadServicios { get; set; }
    [Required(ErrorMessage = "El campo Monto es obligatorio.")]
    public int Monto { get; set; }

    public DateTime Fecha { get; set; }

    public int TecnicoId { get; set; }
    }

