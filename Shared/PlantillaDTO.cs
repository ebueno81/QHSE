using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHSE.Shared
{
    public class PlantillaDTO
    {
        public int IdPlantilla { get; set; }

        public string? Descripcion { get; set; }

        public int? IdArea { get; set; }

        public int? IdCreate { get; set; }

        public string? Area { get; set; }

        public DateTime? FechaCrea { get; set; }

        public virtual List<PlantillaDetDTO>? PlantillaDets { get; set; } 
    }
}
