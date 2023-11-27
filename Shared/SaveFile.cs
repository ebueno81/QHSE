
namespace QHSE.Client.Utilidades
{
    public class SaveFile
    {
        public SaveFile()
        {
            Files = new List<FileData>();
        }
        public List<FileData> Files { get; set; }
    }

    public class FileData
    {
        public byte[] ImageBytes { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
    }
}
