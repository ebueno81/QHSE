using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class UsuaPermiso
{
    public string? CodiUsuario { get; set; }

    public string? CodiModulo { get; set; }

    public bool? PuedeVer { get; set; }

    public bool? PuedeAgregar { get; set; }

    public bool? PuedeEditar { get; set; }

    public bool? PuedeEliminar { get; set; }

    public bool? CAnulaReg { get; set; }

    public string? Usuariocrea { get; set; }

    public string? UsuarioModi { get; set; }

    public DateTime? FechaCrea { get; set; }

    public DateTime? FechaModi { get; set; }
}
