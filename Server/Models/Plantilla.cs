using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class Plantilla
{
    /// <summary>
    /// Id Plantilla
    /// </summary>
    public int IdPlantilla { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Id Area
    /// </summary>
    public int? IdArea { get; set; }

    /// <summary>
    /// Id Tipo Inspección
    /// </summary>
    public int? IdTpoInsp { get; set; }

    /// <summary>
    /// Id Creación
    /// </summary>
    public int? IdCreate { get; set; }

    public virtual Area? IdAreaNavigation { get; set; }

    public virtual Creacion? IdCreateNavigation { get; set; }

    public virtual TpoInspeccion? IdTpoInspNavigation { get; set; }

    public virtual ICollection<PlantillaDet> PlantillaDets { get; set; } = new List<PlantillaDet>();
}
