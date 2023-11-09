using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class TpoUsuario
{
    /// <summary>
    /// Id Tipo Usuario
    /// </summary>
    public int IdTpoUsua { get; set; }

    /// <summary>
    /// Descripción Tipo
    /// </summary>
    public string? DescTpo { get; set; }

    /// <summary>
    /// 1=Activo, 0=Inactivo
    /// </summary>
    public int? Activo { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
