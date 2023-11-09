using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface IUsuarioRepositorio
    {
        Task<List<Usuario>> Lista();
        Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null);
        Task<Creacion> Crear(Creacion entidad);
        Task<bool> Editar(Creacion entidad);
        
        Task<IQueryable<Usuario>> Consultar(Expression<Func<Usuario, bool>> filtro = null);

        Task<List<Usuario>> Login(string usuario, string clave);
        Task<List<Usuario>> ValidarDuplicidad(string usuario);
    }
}
