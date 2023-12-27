using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class EmpresaRepositorio:IEmpresaRepositorio
    {
        private readonly DbqhseContext _dbContext;

        public EmpresaRepositorio(DbqhseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Empresa>> Consultar(Expression<Func<Empresa, bool>> filtro = null)
        {
            IQueryable<Empresa> queryEntidad = filtro == null ? _dbContext.Empresas : _dbContext.Empresas.Where(filtro);
            return queryEntidad;
        }

        public async Task<Empresa> Crear(Empresa entidad)
        {
            try
            {
                _dbContext.Set<Empresa>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Empresa entidad)
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



        public async Task<List<Empresa>> Lista()
        {
            try
            {
                return await _dbContext.Empresas.ToListAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task<Empresa> Obtener(Expression<Func<Empresa, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Empresas.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
