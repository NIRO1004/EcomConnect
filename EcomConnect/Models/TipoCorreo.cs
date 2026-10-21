using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class TipoCorreo
{
    public int IdTipoCorreo { get; set; }

    public string DescripcionCorreo { get; set; } = null!;

    public virtual ICollection<Correo> Correos { get; set; } = new List<Correo>();
}
