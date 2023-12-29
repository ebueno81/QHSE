using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHSE.Shared
{
    public class InspeccionDetDTO
    {
        public int IdInspDet { get; set; }

        public int? IdInsp { get; set; }

        public int? IdSubCtg { get; set; }

        public string? OpcSelect1 { get; set; }

        public string? OpcSelect2 { get; set; }

        public string? Obs1 { get; set; }

        public string? Obs2 { get; set; }

        public byte[]? Foto1 { get; set; }

        public byte[]? Foto2 { get; set; }

        public decimal? NroPctg { get; set; }

        public DateTime? FechaLevanta { get; set; }

        public int? Activo { get; set; }

        public string? SubCategoria { get; set; }

        public string? Categoria { get; set; }

        public string? Estado { get; set; }

        public string? RutaImagen { get; set; }

        public byte[]? Imagen { get; set; }

        public FileStream? Archivo { get; set; }

        public SaveFileDTO? SaveFile { get; set; }

        public string? NroOrden { get; set; }

        public int? IdCtg { get; set; }

        public string? Trabajador { get; set; }

        public string? Area { get; set; }

        public string? Empresa { get; set; }

        public string? Tipo { get; set; }

        public DateTime? Fecha { get; set; }

        public string? SI { get; set; }

        public string? NO { get; set; }

        public string? NA { get; set; }

        public string? NomJefeArea { get; set; }

    }
}
