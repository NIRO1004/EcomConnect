using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Distrito
{
    public int IdDistrito { get; set; }

    public int CantonIdCanton { get; set; }

    public int CantonProvinciaIdProvincia { get; set; }

    public string DescripcionDistrito { get; set; } = null!;

    public int CodigoPostal { get; set; }

    public virtual Canton Canton { get; set; } = null!;

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();
}
