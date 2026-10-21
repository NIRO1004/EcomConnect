using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Correo
{
    public string Correo1 { get; set; } = null!;

    public int TipoCorreoIdTipoCorreo { get; set; }

    public int ClienteIdCliente { get; set; }

    public string ClientePersonaCedula { get; set; } = null!;

    public int ClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual TipoCorreo TipoCorreoIdTipoCorreoNavigation { get; set; } = null!;
}
