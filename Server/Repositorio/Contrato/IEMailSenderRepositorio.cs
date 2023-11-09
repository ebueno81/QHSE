using QHSE.Server.Utilidades;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface IEMailSenderRepositorio
    {
        void SendEmail(Message message);
    }
}
