using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class TipoUsuarioRepositorio : ITipoUsuarioRepositorio
    {
        private readonly DbqhseContext _dbContext;

        public TipoUsuarioRepositorio(DbqhseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<TpoUsuario>> Consultar(Expression<Func<TpoUsuario, bool>> filtro = null)
        {
            IQueryable<TpoUsuario> queryEntidad = filtro == null ? _dbContext.TpoUsuarios : _dbContext.TpoUsuarios.Where(filtro);
            return queryEntidad;
        }

        
    }
    
}
