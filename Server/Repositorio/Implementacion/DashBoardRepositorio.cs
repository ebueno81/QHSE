using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Shared;
using System.Collections.Generic;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class DashBoardRepositorio: IDashBoardRepositorio
    {
        private readonly DbQhseContext _dbContext;
        private readonly IMapper _mapper;

        public DashBoardRepositorio(DbQhseContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Dictionary<string, decimal>> InspeccionUltimaSemana()
        {
            Dictionary<string, decimal> resultado = new Dictionary<string, decimal>();
            try
            {
                IQueryable<Inspeccion> _ventaQuery = _dbContext.Inspeccions.AsQueryable();
                if (_ventaQuery.Count() > 0)
                {
                    int? codigoActa = _dbContext.Acta.Where(x => x.IdCreateNavigation.Activo == 1).
                        OrderByDescending(x => x.FechaProg).Select(v => v.IdActa).FirstOrDefault();

                    if (codigoActa>0)
                    {
                        
                        IQueryable<InspeccionDet> query = _dbContext.InspeccionDets
                            .Include(a=>a.IdInspNavigation.IdAreaNavigation)
                            .Include(x=>x.IdInspNavigation)            
                            .Where(v => v.IdInspNavigation.IdActa== codigoActa && v.IdInspNavigation.IdCreateNavigation.Activo == 1);
                        
                        resultado = query.GroupBy(v => v.IdInspNavigation.IdAreaNavigation.DescArea).OrderBy(g => g.Key)
                            .Select(dv => new { Area = dv.Key.ToString().Substring(0,3), total = dv.Sum(x=>x.OpcSelect1=="1" ? 1 : 0) / Convert.ToDecimal(dv.Count()) })
                            .ToDictionary(keySelector: r => r.Area, elementSelector: r => r.total);
                    }

                }
                return resultado;

            }
            catch
            {

                throw;
            }
        }

        public async Task<int> TotalActas()
        {
            try
            {
                IQueryable<Actum> query = _dbContext.Acta.Where(x => x.IdCreateNavigation.Activo == 1);
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalInspeccion()
        {
            try
            {
                IQueryable<Inspeccion> query = _dbContext.Inspeccions.Where(x => x.IdCreateNavigation.Activo == 1);
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalInspeccionUltima()
        {
            int total = 0;
            try
            {
                IQueryable<Inspeccion> _ventaQuery = _dbContext.Inspeccions.AsQueryable();

                if (_ventaQuery.Count() > 0)
                {
                    DateTime? ultimaFecha = _dbContext.Inspeccions.Where(x => x.IdActa == 1).OrderByDescending(v => v.IdActaNavigation.FechaProg).Select(v => v.IdActaNavigation.FechaProg).FirstOrDefault();

                    if (!string.IsNullOrEmpty(ultimaFecha.ToString()))
                    {
                        ultimaFecha = ultimaFecha.Value.AddDays(-7);

                        IQueryable<Inspeccion> query = _dbContext.Inspeccions.Where(v => v.IdActaNavigation.FechaProg.Value.Date >= ultimaFecha.Value.Date && v.IdCreateNavigation.Activo == 1);
                        total = query.Count();
                    }

                }
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalPlantillas()
        {
            try
            {
                IQueryable<Plantilla> query = _dbContext.Plantillas.Where(x => x.IdCreateNavigation.Activo == 1);
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ResumenDTO>> Consolidado(int idActa)
        {
            
            try
            {
                 List<ResumenDTO> resumen = new List<ResumenDTO>();

                IQueryable<InspeccionDet> query = _dbContext.InspeccionDets
                    .Include(x => x.IdInspNavigation)
                    .Include(a => a.IdInspNavigation.IdAreaNavigation)
                    .Include(e => e.IdInspNavigation.IdEmpNavigation)
                    .Where(ve => ve.IdInspNavigation.IdActa == idActa);

              
                resumen = _mapper.Map<List<ResumenDTO>>(query.GroupBy(v => new { v.IdInspNavigation.IdAreaNavigation.DescArea, v.IdInspNavigation.IdEmpNavigation.RazEmp, v.IdInspNavigation.IdActaNavigation.FechaProg })
                        .Select(dv => new ResumenDTO
                        {
                            Area = dv.Key.DescArea,
                            Empresa = dv.Key.RazEmp,
                            Fecha = dv.Key.FechaProg,
                            Por1 = (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1 : 0) / Convert.ToDecimal(dv.Count())).ToString().Substring(2,2) + "%",
                            Por2 = (dv.Sum(dv => dv.OpcSelect2 == "1" ? 1 : 0) / Convert.ToDecimal(dv.Sum(dv => dv.OpcSelect1 == "0" ? 1 : 0))).ToString().Substring(2,2) + "%",
                            porcEfectivo = (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1 : 0) / Convert.ToDecimal(dv.Count())),
                            Barra = (((dv.Sum(dv => dv.OpcSelect1 == "1" ? 1 : 0) / Convert.ToDecimal(dv.Count())) >= 0 &&
                                    (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) <= 0.1) ? "_"
                                    :
                                    (((dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) > 0.1 &&
                                    (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) <= 0.2) ? "__"
                                    :
                                    (((dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) > 0.2 &&
                                    (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) <= 0.3) ? "___"
                                    :
                                    (((dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) > 0.3 &&
                                    (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) <= 0.4) ? "____"
                                    :
                                    (((dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) > 0.4 &&
                                    (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) <= 0.5) ? "_____"
                                    :
                                    (((dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) > 0.5) &&
                                    (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) <= 0.6) ? "______"
                                    :
                                    (((dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) > 0.6 &&
                                    (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) <= 0.7) ? "_______"
                                    :
                                    (((dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) > 0.7 &&
                                    (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) <= 0.8) ? "________"
                                    :
                                    (((dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) > 0.8 &&
                                    (dv.Sum(dv => dv.OpcSelect1 == "1" ? 1.0 : 0.0) / dv.Count()) <= 0.9) ? "_________"
                                    : "__________"))))))))
                        }));

                


                return resumen;

            }
            catch
            {

                throw;
            }
        }
    }
}
