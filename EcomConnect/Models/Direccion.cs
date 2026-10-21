using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Direccion
{
    public int DistritoIdDistrito { get; set; }

    public int DistritoCantonIdCanton { get; set; }

    public int DistritoCantonProvinciaIdProvincia { get; set; }

    public int TipoDireccionIdTipoDireccion { get; set; }

    public string Detalle { get; set; } = null!;

    public int ClienteIdCliente { get; set; }

    public string ClientePersonaCedula { get; set; } = null!;

    public int ClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Distrito Distrito { get; set; } = null!;

    public virtual TipoDireccion TipoDireccionIdTipoDireccionNavigation { get; set; } = null!;
}
