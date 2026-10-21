using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class CatalogoGenero
{
    public int IdCatalogoGenero { get; set; }

    public string DescripcionGenero { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
