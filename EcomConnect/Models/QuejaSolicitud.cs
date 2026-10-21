using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class QuejaSolicitud
{
    public int IdQuejaSolicitud { get; set; }

    public string Tipo { get; set; } = null!;

    public string DescripcionQuejasolicitud { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public byte[] Estado { get; set; } = null!;

    public string Respuesta { get; set; } = null!;

    public int ClienteIdCliente { get; set; }

    public string ClientePersonaCedula { get; set; } = null!;

    public int ClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
