using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class Trabajador
{
    /// <summary>
    /// Id Trabajador
    /// </summary>
    public int IdTraba { get; set; }

    /// <summary>
    /// Nombre Trabajador
    /// </summary>
    public string? NomTraba { get; set; }

    /// <summary>
    /// Apellido Trabajador
    /// </summary>
    public string? ApeTraba { get; set; }

    /// <summary>
    /// N° Documento
    /// </summary>
    public string? NroDoc { get; set; }

    /// <summary>
    /// N° Telefono
    /// </summary>
    public string? NroTelefono { get; set; }

    /// <summary>
    /// Correo Trabajador
    /// </summary>
    public string? CorreoTraba { get; set; }

    /// <summary>
    /// Id Creación
    /// </summary>
    public int? IdCreate { get; set; }

    public virtual Creacion? IdCreateNavigation { get; set; }

    public virtual ICollection<Inspeccion> Inspeccions { get; set; } = new List<Inspeccion>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
