using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class InspeccionDet
{
    /// <summary>
    /// Id detalle
    /// </summary>
    public int IdInspDet { get; set; }

    /// <summary>
    /// Id Inspección
    /// </summary>
    public int? IdInsp { get; set; }

    /// <summary>
    /// Id Categoria
    /// </summary>
    public int? IdSubCtg { get; set; }

    public string? NroOrden { get; set; }

    /// <summary>
    /// 1° Inspeccion S,N,NA
    /// </summary>
    public string? OpcSelect1 { get; set; }

    /// <summary>
    /// 2° Inspeccion S,N,NA
    /// </summary>
    public string? OpcSelect2 { get; set; }

    /// <summary>
    /// 1° Observación
    /// </summary>
    public string? Obs1 { get; set; }

    /// <summary>
    /// 2° Observación
    /// </summary>
    public string? Obs2 { get; set; }

    /// <summary>
    /// 1° Foto
    /// </summary>
    public byte[]? Foto1 { get; set; }

    /// <summary>
    /// 2° Foto
    /// </summary>
    public byte[]? Foto2 { get; set; }

    /// <summary>
    /// % cumplimiento
    /// </summary>
    public decimal? NroPctg { get; set; }

    /// <summary>
    /// Fecha levantamiento
    /// </summary>
    public DateTime? FechaLevanta { get; set; }

    /// <summary>
    /// 1=Activo, 0=Inactivo
    /// </summary>
    public int? Activo { get; set; }

    public virtual Inspeccion? IdInspNavigation { get; set; }

    public virtual SubCategorium? IdSubCtgNavigation { get; set; }
}
