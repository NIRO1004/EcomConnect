using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public DateOnly FechaPedido { get; set; }

    public decimal Total { get; set; }

    public int ClienteIdCliente { get; set; }

    public string ClientePersonaCedula { get; set; } = null!;

    public int ClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

    public virtual ICollection<RastreoPedido> RastreoPedidos { get; set; } = new List<RastreoPedido>();
}
