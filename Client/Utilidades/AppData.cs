namespace QHSE.Client.Utilidades
{
    public class AppData
    {
        public string empresa { get; set; }

        public string usuarioLogin { get; set; }

        public string usuarioToken { get; set; }

        public string usuarioRol { get; set; }

        public int idUsuario { get; set; }

        public int? idVendedor { get; set; }

        public int? idTraba { get; set; }

        public int? idPedido { get; set; }

        public bool isMobile { get; set; }

        public int xs1 { get; set; }

        public int xs2 { get; set; }

        public int xs3 { get; set; }

        public int xs4 { get; set; }

        public int xs5 { get; set; }

        public int xs6 { get; set; }

        public int xs46 { get; set; }

        public string urlReporte
        {
            //get { return "https://logistica1.somee.com/"; }
            //get { return "https://localhost:7202/"; }
            // get { return "https://hsqvecu.somee.com/"; }
            get; set;
        }
    }
}
