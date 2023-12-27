using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class TipoInspeccionRepositorio:ITipoInspeccionRepositorio
    {
        private readonly DbqhseContext _dbContext;

        public TipoInspeccionRepositorio(DbqhseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<TpoInspeccion>> Consultar(Expression<Func<TpoInspeccion, bool>> filtro = null)
        {
            IQueryable<TpoInspeccion> queryEntidad = filtro == null ? _dbContext.TpoInspeccions : _dbContext.TpoInspeccions.Where(filtro);
            return queryEntidad;
        }
    }
}
