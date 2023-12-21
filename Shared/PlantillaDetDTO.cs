using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QHSE.Shared;

public partial class PlantillaDetDTO
{
    public int IdPlantillaDet { get; set; }

    public int? IdPlantilla { get; set; }

    public int? IdSubCtg { get; set; }

    public string? SubCategoria { get; set; }

    public string? NroOrden { get; set; }

    public string? Categoria { get; set; }

    public int? Activo { get; set; }

    public int? IdCtg { get; set; }

    public string? Estado { get; set; }

    public string? RutaImagen { get; set; }

    public string? Observacion { get; set; }

    //public byte[]? Imagen { get; set; }
    public byte[]? Imagen { get; set; }

    public FileStream? Archivo { get; set; }

    public SaveFileDTO? SaveFile { get; set; }
}
