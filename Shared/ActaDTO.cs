using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHSE.Shared
{
    public class ActaDTO
    {
        public int IdActa { get; set; }

        public string? NroActa { get; set; }

        public DateTime? FechaProg { get; set; }

        public string? Obs { get; set; }

        public int? IdCreate { get; set; }
    }
}
