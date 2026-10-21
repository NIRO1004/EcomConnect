using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Segmento
{
    public int IdSegmento { get; set; }

    public string Nombre { get; set; } = null!;

    public string DescripcionSegmento { get; set; } = null!;

    public string Criterio { get; set; } = null!;

    public byte[] Estado { get; set; } = null!;

    public int ClienteIdCliente { get; set; }

    public string ClientePersonaCedula { get; set; } = null!;

    public int ClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
