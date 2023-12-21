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
    public class AreaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAreaRepositorio _areaRepositorio;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public AreaController(IAreaRepositorio articuloRepositorio, IMapper mapper, IConfiguration configuration, 
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
            ResponseDTO<List<AreaDTO>> _response = new ResponseDTO<List<AreaDTO>>();

            try
            {
                List<AreaDTO> _listaAreas = new List<AreaDTO>();

                IQueryable<Models.Area> query = await _areaRepositorio.Consultar();

                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1);

                _listaAreas = _mapper.Map<List<AreaDTO>>(query.ToList());

                _response = new ResponseDTO<List<AreaDTO>>() { status = true, msg = "ok", value = _listaAreas };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<AreaDTO>>() { status = false, msg = ex.Message, value = null };
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
                Creacion _Area = _mapper.Map<Creacion>(request);

                Creacion _AreaCreado = await _areaRepositorio.Crear(_Area);


                if (_AreaCreado.IdCreate != 0)
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = true, msg = "ok", value = _mapper.Map<CreacionDTO>(_AreaCreado) };
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
                Creacion _Area = _mapper.Map<Creacion>(request);
                Creacion _AreaEditar = await _areaRepositorio.Obtener(u => u.IdCreate == _Area.IdCreate);

                if (_AreaEditar.IdCreate != null)
                {
                   
                    _AreaEditar.FechaModi = DateTime.Now;
                    _AreaEditar.UsuaModi = _Area.UsuaModi;
                    _AreaEditar.PcModi = _Area.PcModi;
                    _AreaEditar.Areas = _Area.Areas;

                    bool respuesta = await _areaRepositorio.Editar(_AreaEditar);

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
        public async Task<IActionResult> imprimirReporte(string? tipoArchivo)
        {
            List<AreaDTO> listaAreas = _mapper.Map<List<AreaDTO>>(await _areaRepositorio.Lista());
            
            FastReport.Report report = new FastReport.Report();
            
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Reportes", "RptAreas.frx");
            report.RegisterData(listaAreas, "DataSet1");
            report.Load(path);
            

            report.SetParameterValue("Titulo","Reporte de Areas");

            report.Prepare();


            using (MemoryStream ms = new MemoryStream())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf");
            }


        }

        [HttpGet]
        [Route("imprimirReporte2")]
        public async Task<IActionResult> imprimirReporte2(string? tipoArchivo)
        {
            List<AreaDTO> listaAreas = _mapper.Map<List<AreaDTO>>(await _areaRepositorio.Lista());

            FastReport.Report report = new FastReport.Report();

            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Reportes", "Reporte.frx");
            report.RegisterData(listaAreas, "DataSet1");
            report.Load(path);


            report.SetParameterValue("Titulo", "Reporte de Areas");

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
