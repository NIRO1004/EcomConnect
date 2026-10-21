using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class RastreoPedido
{
    public int IdRastreo { get; set; }

    public DateOnly FechaActualizacion { get; set; }

    public int EstadoPedidoIdEstadoPedido { get; set; }

    public int PedidoIdPedido { get; set; }

    public int PedidoClienteIdCliente { get; set; }

    public string PedidoClientePersonaCedula { get; set; } = null!;

    public int PedidoClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual EstadoPedido EstadoPedidoIdEstadoPedidoNavigation { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;
}
