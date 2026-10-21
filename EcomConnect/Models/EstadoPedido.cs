using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class EstadoPedido
{
    public int IdEstadoPedido { get; set; }

    public string DescripcionEstado { get; set; } = null!;

    public virtual ICollection<RastreoPedido> RastreoPedidos { get; set; } = new List<RastreoPedido>();
}
