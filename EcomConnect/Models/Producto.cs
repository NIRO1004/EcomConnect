using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string DescripcionProducto { get; set; } = null!;

    public int CantidadDisponible { get; set; }

    public int PuntoReorden { get; set; }

    public short BackOrder { get; set; }

    public decimal Precio { get; set; }

    public int BodegaIdBodega { get; set; }

    public virtual Bodega BodegaIdBodegaNavigation { get; set; } = null!;

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
}
