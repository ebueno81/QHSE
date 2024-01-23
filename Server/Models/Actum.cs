using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class Actum
{
    public int IdActa { get; set; }

    public string? NroActa { get; set; }

    public DateTime? FechaProg { get; set; }

    public string? Obs { get; set; }

    public int? Estado { get; set; }

    public int? IdCreate { get; set; }

    public virtual Creacion? IdCreateNavigation { get; set; }

    public virtual ICollection<Inspeccion> Inspeccions { get; set; } = new List<Inspeccion>();
}
