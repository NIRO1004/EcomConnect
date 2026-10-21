using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class CampanaMarketing
{
    public int IdCampanaMarketing { get; set; }

    public string Nombre { get; set; } = null!;

    public string DescripcionCampanamarketing { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public byte[] Estado { get; set; } = null!;

    public int PromocionIdPromocion { get; set; }

    public int ClienteIdCliente { get; set; }

    public string ClientePersonaCedula { get; set; } = null!;

    public int ClientePersonaTipoPersonaIdtipoPersona { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Promocion PromocionIdPromocionNavigation { get; set; } = null!;
}
