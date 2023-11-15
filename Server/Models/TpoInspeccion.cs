using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class TpoInspeccion
{
    /// <summary>
    /// Id Tipo Inspección
    /// </summary>
    public int IdTpoInsp { get; set; }

    /// <summary>
    /// Descripcion Tipo
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// 1=Activo, 0=Inactivo
    /// </summary>
    public int? Activo { get; set; }

    public virtual ICollection<Inspeccion> Inspeccions { get; set; } = new List<Inspeccion>();
}
