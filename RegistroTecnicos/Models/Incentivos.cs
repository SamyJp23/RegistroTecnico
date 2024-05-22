using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

    public class Incentivos
    {
    [Key]
    public int IncentivoId { get; set; }

    public string Descripcion { get; set; }
    public int CantidadIncentivos { get; set; }
    public int Monto { get; set; }

    public int TecnicoId { get; set; }
    }

