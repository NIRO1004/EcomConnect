
namespace EcomConnect.Models;

public class ClasificacionClienteViewModel
{
    public int IdCliente { get; set; }

    public string Cedula { get; set; } = string.Empty;

    public string NombreCompleto { get; set; } = string.Empty;

    public int CantidadPedidos { get; set; }

    public decimal MontoGastado { get; set; }

    public string ClasificacionFrecuencia { get; set; } = string.Empty;

    public string ClasificacionGasto { get; set; } = string.Empty;

    public int TipoPersonaId { get; set; }

}
