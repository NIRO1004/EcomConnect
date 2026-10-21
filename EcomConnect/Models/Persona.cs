using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Persona
{
    public string Cedula { get; set; } = null!;

    public int IdPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int CatalogoGeneroIdCatalogoGenero { get; set; }

    public int TipoPersonaIdtipoPersona { get; set; }

    public virtual CatalogoGenero CatalogoGeneroIdCatalogoGeneroNavigation { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual TipoPersona TipoPersonaIdtipoPersonaNavigation { get; set; } = null!;
}
