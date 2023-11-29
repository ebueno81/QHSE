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
    }
}
