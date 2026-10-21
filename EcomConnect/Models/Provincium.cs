using System;
using System.Collections.Generic;

namespace EcomConnect.Models;

public partial class Provincium
{
    public int IdProvincia { get; set; }

    public string DescripcionProvincia { get; set; } = null!;

    public virtual ICollection<Canton> Cantons { get; set; } = new List<Canton>();
}
