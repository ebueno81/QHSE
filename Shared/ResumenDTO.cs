using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHSE.Shared
{
    public class ResumenDTO
    {
        public string? Empresa { get; set; }

        public string? Area { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Por1 { get; set; }

        public string? Por2 { get; set; }

        public string? Barra { get; set; }

        public string? OpcSelect1 { get; set; }

        public string? OpcSelect2 { get; set; }

        public decimal? porcEfectivo { get; set; }

    }
}
