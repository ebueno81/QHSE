using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class MnModulo
{
    public string Codigo { get; set; } = null!;

    public string? NombreModulo { get; set; }

    public string? NombreMenu { get; set; }

    public string? NombreTool { get; set; }

    public string? NombreFormulario { get; set; }

    public string? UsuarioCrea { get; set; }

    public string? UsuarioModi { get; set; }

    public DateTime? FechaCrea { get; set; }

    public DateTime? FechaModi { get; set; }

    public int? CAnulaReg { get; set; }
}
