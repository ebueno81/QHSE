﻿using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class Usuario
{
    /// <summary>
    /// Id Usuario
    /// </summary>
    public int IdUsua { get; set; }

    /// <summary>
    /// Nombre Usuario
    /// </summary>
    public string? NomUsua { get; set; }

    /// <summary>
    /// Clave Usuario
    /// </summary>
    public string? ClaveUsua { get; set; }

    /// <summary>
    /// Id Trabajador
    /// </summary>
    public int? IdTrabajador { get; set; }

    /// <summary>
    /// Id Area
    /// </summary>
    public int? IdArea { get; set; }

    /// <summary>
    /// Id Tipo Usuario
    /// </summary>
    public int? IdTpoUsua { get; set; }

    /// <summary>
    /// Id Creación
    /// </summary>
    public int? IdCreate { get; set; }

    public virtual Creacion? IdCreateNavigation { get; set; }

    public virtual TpoUsuario? IdTpoUsuaNavigation { get; set; }
}
