using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Bodega
{
    public int IdBodega { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ubicacion { get; set; } = null!;

    public int TiempoProcesamientoHoras { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
