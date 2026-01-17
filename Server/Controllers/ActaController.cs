using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Shared;
using System.Data;
using FastReport.Export.PdfSimple;



namespace QHSE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActaController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IActaRepositorio _areaRepositorio;
        IWebHostEnvironment _hostingEnvironment;
        readonly IConfiguration _configuration;

        public ActaController(IActaRepositorio articuloRepositorio, IMapper mapper, IConfiguration configuration, 
            IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _areaRepositorio = articuloRepositorio;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<ActaDTO>> _response = new ResponseDTO<List<ActaDTO>>();

            try
            {
                List<ActaDTO> _listaActas = new List<ActaDTO>();

                IQueryable<Models.Actum> query = await _areaRepositorio.Consultar();

                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1);

                _listaActas = _mapper.Map<List<ActaDTO>>(query.ToList());

                _response = new ResponseDTO<List<ActaDTO>>() { status = true, msg = "ok", value = _listaActas.OrderByDescending(x=>x.NroActa).ToList() };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<ActaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] CreacionDTO request)
        {
            ResponseDTO<CreacionDTO> _ResponseDTO = new ResponseDTO<CreacionDTO>();

            try
            {
                Creacion _Acta = _mapper.Map<Creacion>(request);
                _Acta.FechaCrea = DateTime.Now;

                Creacion _ActaCreado = await _areaRepositorio.Crear(_Acta);

                if (_ActaCreado.IdCreate != 0)
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = true, msg = "ok", value = _mapper.Map<CreacionDTO>(_ActaCreado) };
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

        [HttpPut]
        [Authorize]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] CreacionDTO request)
        {
            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();

            try
            {
                Creacion _Acta = _mapper.Map<Creacion>(request);
                Creacion _ActaEditar = await _areaRepositorio.Obtener(u => u.IdCreate == _Acta.IdCreate);

                if (_ActaEditar.IdCreate != null)
                {
                    _ActaEditar.FechaModi = DateTime.Now;
                    _ActaEditar.UsuaModi = _Acta.UsuaModi;
                    _ActaEditar.PcModi = _Acta.PcModi;
                    _ActaEditar.Acta = _Acta.Acta;

                    bool respuesta = await _areaRepositorio.Editar(_ActaEditar);

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
        public async Task<IActionResult> imprimirReporte(int? codigo)
        {
            List<ActaDTO> listaActas = _mapper.Map<List<ActaDTO>>(await _areaRepositorio.Consultar(x=>x.IdActa==codigo));
            
            FastReport.Report report = new FastReport.Report();
            
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Reportes", "RptActa.frx");
            report.RegisterData(listaActas, "DataSet1");
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
