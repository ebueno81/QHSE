
namespace QHSE.Shared
{
    public class CreacionDTO
    {
        public int IdCreate { get; set; }

        public int? Activo { get; set; }

        public string? UsuaCrea { get; set; }

        public string? UsuaModi { get; set; }

        public string? UsuaAnula { get; set; }

        public DateTime? FechaCrea { get; set; }

        public DateTime? FechaModi { get; set; }

        public DateTime? FechaAnula { get; set; }

        public string? PcCrea { get; set; }

        public string? PcModi { get; set; }

        public string? PcAnula { get; set; }

    //   public virtual List<PersonaDTO>? Personas { get; set; }

        public virtual List<AreaDTO>? Areas { get; set; }

        //public virtual List<VendedorDTO>? Vendedores { get; set; }

        public virtual List<UsuarioDTO>? Usuarios { get; set; }

        //public virtual List<PedidoDTO>? Pedidos { get; set; }

        
    }
}
