using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class ActaRepositorio : IActaRepositorio

    {
        private readonly DbQhseContext _dbContext;

        public ActaRepositorio(DbQhseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Actum>> Consultar(Expression<Func<Actum, bool>> filtro = null)
        {
            IQueryable<Actum> queryEntidad = filtro == null ? _dbContext.Acta : _dbContext.Acta.Where(filtro);
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

        public async Task<List<Actum>> Lista()
        {
            try
            {
                return await _dbContext.Acta.ToListAsync();
            }
            catch
            {

                throw;
            }
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
