using System;
using System.Collections.Generic;

namespace QHSE.Server.Models;

public partial class Empresa
{
    /// <summary>
    /// Id Empresa
    /// </summary>
    public int IdEmp { get; set; }

    /// <summary>
    /// Ruc Empresa
    /// </summary>
    public string? RucEmp { get; set; }

    /// <summary>
    /// Razon Social Emp
    /// </summary>
    public string? RazEmp { get; set; }

    /// <summary>
    /// Dirección Empresa
    /// </summary>
    public string? DirecEmp { get; set; }

    /// <summary>
    /// N° Telefono
    /// </summary>
    public string? NroTelefono { get; set; }

    /// <summary>
    /// Correo Empresa
    /// </summary>
    public string? CorreoEmp { get; set; }

    /// <summary>
    /// Departamento Emp
    /// </summary>
    public string? DptEmp { get; set; }

    /// <summary>
    /// Provincia Empresa
    /// </summary>
    public string? ProvEmp { get; set; }

    /// <summary>
    /// Distrito Empresa
    /// </summary>
    public string? DistEmp { get; set; }

    public string? ActividadEmp { get; set; }

    public int? CantTrabEmp { get; set; }

    public string? JefeSstr { get; set; }

    public string? JefePlanta { get; set; }

    public string? JefeComite { get; set; }

    public virtual ICollection<Inspeccion> Inspeccions { get; set; } = new List<Inspeccion>();
}
