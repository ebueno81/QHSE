using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHSE.Shared
{
    public class InspeccionDTO
    {
        public int IdInsp { get; set; }

        public int? IdActa { get; set; }

        public int? IdTpoInsp { get; set; }

        public int? IdArea { get; set; }

        public int? IdSuper1 { get; set; }

        public int? IdSuper2 { get; set; }

        public string? NomSuper2 { get; set; }

        public DateTime? Fecha { get; set; }

        public int? IdJefeArea { get; set; }

        public int? IdEmp { get; set; }

        public int? IdCreate { get; set; }

        public string? Area { get; set; }

        public DateTime? FechaCrea { get; set; }

        public string? NomSuper1 { get; set; }

        public string? Inspeccion { get; set; }

         public virtual List<InspeccionDetDTO> InspeccionDets { get; set; }

        public string? NomJefeArea { get; set; }

        public byte[]? FirmaJefeArea { get; set; }

    }
}
