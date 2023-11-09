using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class Categorium
{
    /// <summary>
    /// Id Categoria
    /// </summary>
    public int IdCtg { get; set; }

    /// <summary>
    /// Desccripcion
    /// </summary>
    public string? DescCtg { get; set; }

    /// <summary>
    /// Id Creacion
    /// </summary>
    public int? IdCreate { get; set; }

    public virtual Creacion? IdCreateNavigation { get; set; }

    public virtual ICollection<SubCategorium> SubCategoria { get; set; } = new List<SubCategorium>();
}
