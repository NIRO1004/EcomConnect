using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Telefono
{
    public int IdTelefono { get; set; }

    public int TipoTelefonoIdTipoTelefono { get; set; }

    public int ClienteIdCliente { get; set; }

    public string ClientePersonaCedula { get; set; } = null!;

    public int ClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual TipoTelefono TipoTelefonoIdTipoTelefonoNavigation { get; set; } = null!;
}
