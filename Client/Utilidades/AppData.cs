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

        public int xs1 { get; set; } = 1;

        public int xs2 { get; set; } = 2;

        public int xs3 { get; set; } = 3;

        public int xs4 { get; set; } = 4;

        public int xs5 { get; set; }=5;

        public int xs6 { get; set; } = 6;

        public int xs46 { get; set; } = 4;

        public int nFilas { get; set; } = 10;

        public string urlReporte
        {
            //get { return "https://logistica1.somee.com/"; }
            //get { return "https://localhost:7202/"; }
            // get { return "https://hsqvecu.somee.com/"; }
            get; set;
        }
    }
}
