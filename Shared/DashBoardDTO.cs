namespace QHSE.Shared
{
    public class DashBoardDTO
    {
        public int TotalInspeccion { get; set; }
        public int? TotalPlantillas { get; set; }
        public int TotalActas { get; set; }
        public List<InspeccionSemanaDTO>? InspeccionUltimaSemana { get; set; }
    }
}
