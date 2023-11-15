using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHSE.Shared
{
    public class TipoInspeccionDTO
    {
        public int IdTpoInsp { get; set; }

        public string? Descripcion { get; set; }

        public int? Activo { get; set; }
    }
}
