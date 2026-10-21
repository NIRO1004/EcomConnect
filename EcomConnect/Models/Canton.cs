using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Canton
{
    public int IdCanton { get; set; }

    public int ProvinciaIdProvincia { get; set; }

    public string DescipcionCanton { get; set; } = null!;

    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

    public virtual Provincium ProvinciaIdProvinciaNavigation { get; set; } = null!;
}
