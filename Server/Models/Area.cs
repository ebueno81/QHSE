using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class Area
{
    /// <summary>
    /// Id Area
    /// </summary>
    public int IdArea { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    public string? DescArea { get; set; }

    /// <summary>
    /// Id Creación
    /// </summary>
    public int? IdCreate { get; set; }

    public virtual Creacion? IdCreateNavigation { get; set; }

    public virtual ICollection<Inspeccion> Inspeccions { get; set; } = new List<Inspeccion>();

    public virtual ICollection<Plantilla> Plantillas { get; set; } = new List<Plantilla>();
}
