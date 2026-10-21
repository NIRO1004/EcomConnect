using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class TipoDireccion
{
    public int IdTipoDireccion { get; set; }

    public string DescripcionTipoDireccion { get; set; } = null!;

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();
}
