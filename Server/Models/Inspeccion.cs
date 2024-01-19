using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class Inspeccion
{
    /// <summary>
    /// Id Inspección
    /// </summary>
    public int IdInsp { get; set; }

    /// <summary>
    /// Id Tipo Inspección
    /// </summary>
    public int? IdTpoInsp { get; set; }

    /// <summary>
    /// Id Area
    /// </summary>
    public int? IdArea { get; set; }

    /// <summary>
    /// Id Trabajador 1
    /// </summary>
    public int? IdSuper1 { get; set; }

    /// <summary>
    /// Id Trabajador 2
    /// </summary>
    public int? IdSuper2 { get; set; }

    /// <summary>
    /// Nombre trabajador 2
    /// </summary>
    public string? NomSuper2 { get; set; }

    public int? IdJefeArea { get; set; }

    public string? NomJefeArea { get; set; }

    public byte[]? FirmaJefeArea { get; set; }

    /// <summary>
    /// Fecha Inspección
    /// </summary>
    public DateTime? Fecha { get; set; }

    /// <summary>
    /// Id Empresa
    /// </summary>
    public int? IdEmp { get; set; }

    /// <summary>
    /// Id Creación
    /// </summary>
    public int? IdCreate { get; set; }

    public virtual Area? IdAreaNavigation { get; set; }

    public virtual Creacion? IdCreateNavigation { get; set; }

    public virtual Empresa? IdEmpNavigation { get; set; }

    public virtual Trabajador? IdSuper1Navigation { get; set; }

    public virtual TpoInspeccion? IdTpoInspNavigation { get; set; }

    public virtual ICollection<InspeccionDet> InspeccionDets { get; set; } = new List<InspeccionDet>();
}
