using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class TrabajadorRepositorio : ITrabajadorRepositorio
    {
        private readonly DbQhseContext _dbContext;

        public TrabajadorRepositorio(DbQhseContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
