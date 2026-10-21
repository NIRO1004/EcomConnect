using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Preferencium
{
    public int IdPreferencia { get; set; }

    public string Tipo { get; set; } = null!;

    public string DescripcionPreferencia { get; set; } = null!;

    public int ClienteIdCliente { get; set; }

    public string ClientePersonaCedula { get; set; } = null!;

    public int ClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
