using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class AreaRepositorio : IAreaRepositorio

    {
        private readonly DbQhseContext _dbContext;

        public AreaRepositorio(DbQhseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Area>> Consultar(Expression<Func<Area, bool>> filtro = null)
        {
            IQueryable<Area> queryEntidad = filtro == null ? _dbContext.Areas : _dbContext.Areas.Where(filtro);
            return queryEntidad;
        }

        public async Task<Creacion> Crear(Creacion entidad)
        {
            try
            {
                _dbContext.Set<Creacion>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Creacion entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public Task<List<Area>> Lista()
        {
            throw new NotImplementedException();
        }

        public async Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Creacions.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
