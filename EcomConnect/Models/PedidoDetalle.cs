using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class PedidoDetalle
{
    public int IdPedidoDetalle { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal SubTotal { get; set; }

    public int ProductoIdProducto { get; set; }

    public int ProductoBodegaIdBodega { get; set; }

    public int PedidoIdPedido { get; set; }

    public int PedidoClienteIdCliente { get; set; }

    public string PedidoClientePersonaCedula { get; set; } = null!;

    public int PedidoClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
