using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public DateOnly FechaAlta { get; set; }

    public DateOnly? FechaBaja { get; set; }

    public string MetodoPago { get; set; } = null!;

    public decimal Calificacion { get; set; }

    public string PersonaCedula { get; set; } = null!;

    public int PersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual ICollection<CampanaMarketing> CampanaMarketings { get; set; } = new List<CampanaMarketing>();

    public virtual ICollection<Correo> Correos { get; set; } = new List<Correo>();

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    public virtual ICollection<EstadoCliente> EstadoClientes { get; set; } = new List<EstadoCliente>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Persona Persona { get; set; } = null!;

    public virtual ICollection<Preferencium> Preferencia { get; set; } = new List<Preferencium>();

    public virtual ICollection<QuejaSolicitud> QuejaSolicituds { get; set; } = new List<QuejaSolicitud>();

    public virtual ICollection<Segmento> Segmentos { get; set; } = new List<Segmento>();

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}
