using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class Creacion
{
    /// <summary>
    /// Id Creación
    /// </summary>
    public int IdCreate { get; set; }

    /// <summary>
    /// Usuario Crea
    /// </summary>
    public string? UsuaCrea { get; set; }

    /// <summary>
    /// Usuario Modifica
    /// </summary>
    public string? UsuaModi { get; set; }

    /// <summary>
    /// Usuario Anula
    /// </summary>
    public string? UsuaAnula { get; set; }

    /// <summary>
    /// Fecha Crea
    /// </summary>
    public DateTime? FechaCrea { get; set; }

    /// <summary>
    /// Fecha Modifica
    /// </summary>
    public DateTime? FechaModi { get; set; }

    /// <summary>
    /// Fecha Anula
    /// </summary>
    public DateTime? FechaAnula { get; set; }

    /// <summary>
    /// PC Crea
    /// </summary>
    public string? PcCrea { get; set; }

    /// <summary>
    /// PC Modifica
    /// </summary>
    public string? PcModi { get; set; }

    /// <summary>
    /// PC Anula
    /// </summary>
    public string? PcAnula { get; set; }

    /// <summary>
    /// 1=Activo, 0=Inactivo
    /// </summary>
    public int? Activo { get; set; }

    public virtual ICollection<Actum> Acta { get; set; } = new List<Actum>();

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();

    public virtual ICollection<Categorium> Categoria { get; set; } = new List<Categorium>();

    public virtual ICollection<Inspeccion> Inspeccions { get; set; } = new List<Inspeccion>();

    public virtual ICollection<Plantilla> Plantillas { get; set; } = new List<Plantilla>();

    public virtual ICollection<SubCategorium> SubCategoria { get; set; } = new List<SubCategorium>();

    public virtual ICollection<Trabajador> Trabajadors { get; set; } = new List<Trabajador>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
