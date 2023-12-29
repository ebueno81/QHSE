using AutoMapper;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Server.Repositorio.Implementacion;
using QHSE.Shared;

namespace QHSE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspeccionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IInspeccionRepositorio _InspeccionRepositorio;
        private IWebHostEnvironment _hostingEnvironment;

        public InspeccionController(IInspeccionRepositorio InspeccionRepositorio, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _InspeccionRepositorio = InspeccionRepositorio;
            _hostingEnvironment = hostingEnvironment;   
        }

        [Authorize]
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista(int? codigoInspeccion)
        {
            ResponseDTO<List<InspeccionDTO>> _response = new ResponseDTO<List<InspeccionDTO>>();

            try
            {
                List<InspeccionDTO> _listaInspeccions = new List<InspeccionDTO>();


                IQueryable<Inspeccion> query = await _InspeccionRepositorio.Consultar(codigoInspeccion > 0 ? x => x.IdInsp == codigoInspeccion : null);


                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1)
                        .Include(a => a.IdAreaNavigation)
                        .Include(t => t.IdTpoInspNavigation)
                        .Include(p => p.IdSuper1Navigation);

                _listaInspeccions = _mapper.Map<List<InspeccionDTO>>(query.ToList());


                _response = new ResponseDTO<List<InspeccionDTO>>() { status = true, msg = "ok", value = _listaInspeccions };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<InspeccionDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [Authorize]
        [HttpGet]
        [Route("ListaDetalles")]
        public async Task<IActionResult> ListaDetalles(int codigoInspeccion, int numVerificacion)
        {
            ResponseDTO<List<InspeccionDetDTO>> _response = new ResponseDTO<List<InspeccionDetDTO>>();

            try
            {
                List<InspeccionDetDTO> _listaInspeccion = new List<InspeccionDetDTO>();

                IQueryable<InspeccionDet> query = await _InspeccionRepositorio.ConsultarDetalle(codigoInspeccion, numVerificacion);
                query = query.Include(ca => ca.IdSubCtgNavigation)
                    .Where(r => r.Activo == 1)
                    .Include(s => s.IdSubCtgNavigation)
                    .Include(ca => ca.IdSubCtgNavigation.IdCtgNavigation)
                    .Include(a => a.IdInspNavigation.IdAreaNavigation)
                    .Include(e => e.IdInspNavigation.IdEmpNavigation)
                    .Include(tr => tr.IdInspNavigation.IdSuper1Navigation)
                    .Include(t => t.IdInspNavigation.IdTpoInspNavigation);

                _listaInspeccion = _mapper.Map<List<InspeccionDetDTO>>(query.ToList());

                if (_listaInspeccion.Count > 0)
                    _response = new ResponseDTO<List<InspeccionDetDTO>>() { status = true, msg = "ok", value = _listaInspeccion.ToList() };
                else
                    _response = new ResponseDTO<List<InspeccionDetDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<InspeccionDetDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [Authorize]
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] CreacionDTO request)
        {

            ResponseDTO<CreacionDTO> _ResponseDTO = new ResponseDTO<CreacionDTO>();

            try
            {
                request.Activo = 1;
                request.FechaCrea = DateTime.Now;

                Creacion _Inspeccion = _mapper.Map<Creacion>(request);

                Creacion _InspeccionCreado = await _InspeccionRepositorio.Crear(_Inspeccion);


                if (_InspeccionCreado.IdCreate != 0)
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = true, msg = "ok", value = _mapper.Map<CreacionDTO>(_InspeccionCreado) };
                else
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = false, msg = "No se pudo crear el registro" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);

            }
            catch (Exception ex)
            {

                _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }


        }

        [Authorize]
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] CreacionDTO request)
        {

            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();

            try
            {
                Creacion _Inspeccion = _mapper.Map<Creacion>(request);
                Creacion _InspeccionEditar = await _InspeccionRepositorio.Obtener(u => u.IdCreate == _Inspeccion.IdCreate);

                if (_InspeccionEditar.IdCreate != null)
                {

                    _InspeccionEditar.FechaModi = DateTime.Now;
                    _InspeccionEditar.UsuaModi = _Inspeccion.UsuaModi;
                    _InspeccionEditar.PcModi = _Inspeccion.PcModi;
                    _InspeccionEditar.Inspeccions = _Inspeccion.Inspeccions;

                    bool respuesta = await _InspeccionRepositorio.Editar(_InspeccionEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo crear el registro" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el registro" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);

            }
            catch (Exception ex)
            {

                _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }


        }

        [HttpGet]
        [Route("imprimirReporte")]
        public async Task<IActionResult> imprimirReporte(int codigoInspeccion, int numVerificacion)
        {
            List<InspeccionDetDTO> _listaRegistros = new List<InspeccionDetDTO>();

            IQueryable<InspeccionDet> query = await _InspeccionRepositorio.ConsultarDetalle(codigoInspeccion, numVerificacion);
            query = query.Include(c => c.IdInspNavigation)
                    .Include(s => s.IdSubCtgNavigation)
                    .Include(ca => ca.IdSubCtgNavigation.IdCtgNavigation)
                    .Include(a => a.IdInspNavigation.IdAreaNavigation)
                    .Include(e => e.IdInspNavigation.IdEmpNavigation)
                    .Include(t => t.IdInspNavigation.IdSuper1Navigation)
                    .Include(t => t.IdInspNavigation.IdTpoInspNavigation)
                    ;

            _listaRegistros = _mapper.Map<List<InspeccionDetDTO>>(query.ToList());

            FastReport.Report report = new FastReport.Report();

            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Reportes", "RptInspeccion.frx");
            report.RegisterData(_listaRegistros, "DataSet1");
            report.Load(path);

            //report.SetParameterValue("Titulo", "Reporte de Areas Hoy");

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
