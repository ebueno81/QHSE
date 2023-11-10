namespace QHSE.Shared
{
    public class TrabajadorDTO
    {
        public int IdVendedor { get; set; }

        public int? IdTraba { get; set; }

        public int? IdCargo { get; set; }

        public string? Detalles { get; set; }

        public string? TipoCargo { get; set; }

        public string? Nombres { get; set; }

        public int? IdCreate { get; set; }

        public string? UsuaCrea { get; set; }

        public string? UsuaModi { get; set; }

        public DateTime? FechaCrea { get; set; }

        public DateTime? FechaModi { get; set; }

        public string? PcCrea { get; set; }

        public string? PcModi { get; set; }

        public string? Correo { get; set; }

        public int? IdTipoPerso { get; set; }
    }
}
