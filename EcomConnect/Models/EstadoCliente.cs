using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class EstadoCliente
{
    public int IdEstadoCliente { get; set; }

    public byte[] Estado { get; set; } = null!;

    public DateOnly FechaCambioEstado { get; set; }

    public int ClienteIdCliente { get; set; }

    public string ClientePersonaCedula { get; set; } = null!;

    public int ClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
