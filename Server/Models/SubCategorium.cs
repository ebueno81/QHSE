using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class SubCategorium
{
    /// <summary>
    /// Id Sub Categoria
    /// </summary>
    public int IdSubCtg { get; set; }

    /// <summary>
    /// Id Categoria
    /// </summary>
    public int? IdCtg { get; set; }

    /// <summary>
    /// Desc Sub Categoria
    /// </summary>
    public string? DescSubCtg { get; set; }

    /// <summary>
    /// Id Creción
    /// </summary>
    public int? IdCreate { get; set; }

    public virtual Creacion? IdCreateNavigation { get; set; }

    public virtual Categorium? IdCtgNavigation { get; set; }

    public virtual ICollection<PlantillaDet> PlantillaDets { get; set; } = new List<PlantillaDet>();
}
