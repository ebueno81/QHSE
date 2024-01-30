using AutoMapper;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Shared;

namespace QHSE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDashBoardRepositorio _dashboardRepositorio;
        private IWebHostEnvironment _hostingEnvironment;

        public DashBoardController(IDashBoardRepositorio dashboardRepositorio, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _dashboardRepositorio = dashboardRepositorio;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Authorize]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            ResponseDTO<DashBoardDTO> _response = new ResponseDTO<DashBoardDTO>();

            try
            {

                DashBoardDTO vmDashboard = new DashBoardDTO();

                vmDashboard.TotalInspeccion = await _dashboardRepositorio.TotalInspeccion();
                vmDashboard.TotalActas = await _dashboardRepositorio.TotalActas();
                vmDashboard.TotalPlantillas = await _dashboardRepositorio.TotalPlantillas();

                List<InspeccionSemanaDTO> listaVentasSemana = new List<InspeccionSemanaDTO>();

                foreach (KeyValuePair<string, decimal> item in await _dashboardRepositorio.InspeccionUltimaSemana())
                {
                    listaVentasSemana.Add(new InspeccionSemanaDTO()
                    {
                        Area = item.Key,
                        Total = item.Value
                    });
                }
                vmDashboard.InspeccionUltimaSemana = listaVentasSemana;

                // if (vmDashboard.VentasUltimaSemana.Count()>0)
                _response = new ResponseDTO<DashBoardDTO>() { status = true, msg = "ok", value = vmDashboard };
                //else
                //    _response = new ResponseDTO<DashBoardDTO>() { status = false, msg = "no existen registros", value = null };

                return StatusCode(StatusCodes.Status200OK, _response);

            }
            catch (Exception ex)
            {
                _response = new ResponseDTO<DashBoardDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [HttpGet]
        [Route("imprimirConsolidado")]
        public async Task<IActionResult> imprimirConsolidado(int codigo)
        {
            List<ResumenDTO> vmResumen2 = new List<ResumenDTO>();
            vmResumen2 = await _dashboardRepositorio.Consolidado(codigo);

            FastReport.Report report = new FastReport.Report();

            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Reportes", "RptResumen.frx");
            report.RegisterData(vmResumen2, "DataSet1");
            report.Load(path);

            report.Prepare();


            using (MemoryStream ms = new MemoryStream())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf");
            }

        }

       
    }
}
