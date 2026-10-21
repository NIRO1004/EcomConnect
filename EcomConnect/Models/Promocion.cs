using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Promocion
{
    public int IdPromocion { get; set; }

    public string Nombre { get; set; } = null!;

    public string DescripcionPromocion { get; set; } = null!;

    public decimal Descuento { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public byte[] Estado { get; set; } = null!;

    public virtual ICollection<CampanaMarketing> CampanaMarketings { get; set; } = new List<CampanaMarketing>();
}
