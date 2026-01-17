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
            IQueryable<InspeccionDet> query = _dbContext.InspeccionDets
                .Include(x => x.IdInspNavigation)
                .Include(x => x.IdInspNavigation.IdAreaNavigation)
                .Include(x => x.IdInspNavigation.IdEmpNavigation)
                .Where(x => x.IdInspNavigation.IdActa == idActa);

            var resumen = query
                .GroupBy(v => new
                {
                    v.IdInspNavigation.IdAreaNavigation.DescArea,
                    v.IdInspNavigation.IdEmpNavigation.RazEmp,
                    v.IdInspNavigation.IdActaNavigation.FechaProg
                })
                .Select(dv => new
                {
                    dv.Key,
                    Total = dv.Count(),
                    Ok1 = dv.Sum(x => x.OpcSelect1 == "1" ? 1 : 0),
                    Ok2 = dv.Sum(x => x.OpcSelect2 == "1" ? 1 : 0)
                })
                .AsEnumerable() // ← evita problemas de traducción EF
                .Select(x =>
                {
                    decimal porcentaje = x.Total == 0
                        ? 0
                        : (decimal)(x.Ok1 + x.Ok2) / x.Total;

                    return new ResumenDTO
                    {
                        Area = x.Key.DescArea,
                        Empresa = x.Key.RazEmp,
                        Fecha = x.Key.FechaProg,

                        Por1 = ((decimal)x.Ok1 / x.Total).ToString("P0"),

                        Por2 = porcentaje.ToString("P0"),

                        porcEfectivo = porcentaje,

                        Barra =
                            porcentaje <= 0.1m ? "_" :
                            porcentaje <= 0.2m ? "___" :
                            porcentaje <= 0.3m ? "_____" :
                            porcentaje <= 0.4m ? "______" :
                            porcentaje <= 0.5m ? "_______" :
                            porcentaje <= 0.6m ? "________" :
                            porcentaje <= 0.7m ? "_________" :
                            porcentaje <= 0.8m ? "__________" :
                            porcentaje <= 0.9m ? "___________" :
                                                 "_____________"
                    };
                })
                .ToList();

            return resumen;
        }
    }
}
