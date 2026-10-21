using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateOnly FechaFactura { get; set; }

    public decimal Total { get; set; }

    public int MetodoPagoIdMetodoPago { get; set; }

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual MetodoPago MetodoPagoIdMetodoPagoNavigation { get; set; } = null!;
}
