using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class TrabajadorRepositorio : ITrabajadorRepositorio
    {
        private readonly DbQhseContext _dbContext;

        public TrabajadorRepositorio(DbQhseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Trabajador>> Consultar(Expression<Func<Trabajador, bool>> filtro = null)
        {
            IQueryable<Trabajador> queryEntidad = filtro == null ? _dbContext.Trabajadors : _dbContext.Trabajadors.Where(filtro);
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


        public async Task<List<Trabajador>> Lista()
        {
            try
            {
                return await _dbContext.Trabajadors.ToListAsync();
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
