using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class TipoTelefono
{
    public int IdTipoTelefono { get; set; }

    public string DescripcionTipotelefono { get; set; } = null!;

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}
