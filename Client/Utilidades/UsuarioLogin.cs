using System.ComponentModel.DataAnnotations;

namespace QHSE.Client.Utilidades
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "El correo es requerido.")]
        public string Usuario1 { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Clave { get; set; } = null!;
    }
}
