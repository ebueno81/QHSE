using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class PlantillaDet
{
    /// <summary>
    /// Id Detalle Plantilla
    /// </summary>
    public int IdPlantillaDet { get; set; }

    /// <summary>
    /// Id Plantilla
    /// </summary>
    public int? IdPlantilla { get; set; }

    /// <summary>
    /// Id Sub Categoria
    /// </summary>
    public int? IdSubCtg { get; set; }

    /// <summary>
    /// 1=Activo, 0=Inactivo
    /// </summary>
    public int? Activo { get; set; }

    public virtual Plantilla? IdPlantillaNavigation { get; set; }

    public virtual SubCategorium? IdSubCtgNavigation { get; set; }
}
