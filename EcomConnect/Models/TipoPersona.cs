using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class TipoPersona
{
    public int IdtipoPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public string DescripcionTipopersona { get; set; } = null!;

    public byte[] Estado { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
