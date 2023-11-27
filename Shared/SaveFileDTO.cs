using QHSE.Client.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHSE.Shared
{
    public class SaveFileDTO
    {
        public SaveFileDTO()
        {
            Files = new FileDataDTO();
        }
        public FileDataDTO Files { get; set; }
    }

    public class FileDataDTO
    {
        public byte[] ImageBytes { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
    }
}
