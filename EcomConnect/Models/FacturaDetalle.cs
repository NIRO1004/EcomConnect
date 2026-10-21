using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class FacturaDetalle
{
    public int IdFacturaDetalle { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal SubTotal { get; set; }

    public int ProductoIdProducto { get; set; }

    public int ProductoBodegaIdBodega { get; set; }

    public int FacturaIdFactura { get; set; }

    public virtual Factura FacturaIdFacturaNavigation { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
