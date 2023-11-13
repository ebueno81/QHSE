using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Shared;

namespace QHSE.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAreaRepositorio _areaRepositorio;

        public AreaController(IAreaRepositorio articuloRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _areaRepositorio = articuloRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<AreaDTO>> _response = new ResponseDTO<List<AreaDTO>>();

            try
            {
                List<AreaDTO> _listaAreas = new List<AreaDTO>();

                IQueryable<Area> query = await _areaRepositorio.Consultar();
                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1);

                _listaAreas = _mapper.Map<List<AreaDTO>>(query.ToList());

                if (_listaAreas.Count > 0)
                    _response = new ResponseDTO<List<AreaDTO>>() { status = true, msg = "ok", value = _listaAreas };
                else
                    _response = new ResponseDTO<List<AreaDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<AreaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [HttpPost]
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
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = false, msg = "No se pudo crear el vendedor" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);

            }
            catch (Exception ex)
            {

                _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }


        }

        [HttpPut]
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
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo crear el vendedor" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el vendedor" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);

            }
            catch (Exception ex)
            {

                _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }


        }



    }
}
